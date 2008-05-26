using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Com.Prerit.Domain;
using Com.Prerit.Services;

public partial class photo_albums_default : Page
{
    #region Constants

    private const string _albumNameQueryStringKey = "album_name";

    private const string _albumYearQueryStringKey = "album_year";

    #endregion

    #region Properties

    public string AlbumNameQueryStringValue
    {
        get { return Request.QueryString[_albumNameQueryStringKey]; }
    }

    public int? AlbumYearQueryStringValue
    {
        get
        {
            int parsedAlbumYear;

            if (int.TryParse(Request.QueryString[_albumYearQueryStringKey], out parsedAlbumYear))
            {
                return parsedAlbumYear;
            }

            return null;
        }
    }

    #endregion

    #region Methods

    private void AddLightboxCss()
    {
        HtmlLink link = new HtmlLink();

        link.Attributes.Add("rel", "stylesheet");
        link.Attributes.Add("type", "text/css");
        link.Attributes.Add("href", "~/lightbox/css/lightbox.css");
        link.Attributes.Add("media", "screen");

        Header.Controls.Add(link);
    }

    private void AddNoCacheMetaTag()
    {
        HtmlMeta htmlMeta = new HtmlMeta();

        htmlMeta.HttpEquiv = "pragma";
        htmlMeta.Content = "no-cache";

        Header.Controls.Add(htmlMeta);
    }

    protected void AlbumYearRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
            case ListItemType.Item:
                AlbumYear albumYear = (AlbumYear) e.Item.DataItem;

                Debug.Assert(albumYear.Albums != null);

                if (albumYear.Albums.Length == 0)
                {
                    HideAlbumYearPlaceHolder(e);
                }
                break;
        }
    }

    [WebMethod]
    public static int GetLoaderServiceState()
    {
        IImageEditorService imageEditorService = new ImageEditorService();
        IAlbumYearLoaderService albumYearLoaderService = new AlbumYearLoaderService("~/photo_albums/", imageEditorService);
        IAsyncCacheItemLoaderService asyncCacheItemLoaderService = new AsyncCacheItemLoaderService();
        ILoaderAsyncService<AlbumYear[]> photoAlbumLoaderService = new PhotoAlbumLoaderService(albumYearLoaderService, asyncCacheItemLoaderService);

        if (photoAlbumLoaderService.IsLoading())
        {
            return 1;
        }

        if (photoAlbumLoaderService.IsFailedLoad())
        {
            return 2;
        }

        return 3;
    }

    public string ConvertNameToTitle(string name)
    {
        string albumNameAsTitle = null;

        if (name != null)
        {
            string[] words = name.Split('_');

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                bool isEmptyString = word.Length == 0;

                if (!isEmptyString)
                {
                    char[] letters = word.ToCharArray();

                    letters[0] = char.ToUpper(letters[0]);

                    word = new string(letters);

                    words[i] = word;
                }
            }

            albumNameAsTitle = string.Join(" ", words);
        }

        return albumNameAsTitle;
    }

    private void EnablePhotoAnimations()
    {
        AddLightboxCss();
        RegisterLightboxClientScriptIncludes();
    }

    private AlbumYear GetAlbumYearRepeaterDataSource(int albumYear, IPhotoAlbumFinderService photoAlbumFinderService)
    {
        return photoAlbumFinderService.FindAlbumYear(albumYear);
    }

    private IEnumerable<AlbumYear> GetAlbumYearRepeaterDataSource(AlbumYear[] albumYears)
    {
        return OrderChronologically(albumYears);
    }

    protected string GetLightboxImageSetIdentifier()
    {
        return PreventLightboxImageSetIdentifierBug(AlbumNameQueryStringValue);
    }

    private Photo[] GetPhotoRepeaterDataSource(int albumYear, string albumName, IPhotoAlbumFinderService photoAlbumFinderService)
    {
        return photoAlbumFinderService.FindPhotos(albumYear, albumName);
    }

    private void HideAlbumYearPlaceHolder(RepeaterItemEventArgs e)
    {
        Debug.Assert(e != null);

        PlaceHolder albumYearPlaceHolder = (PlaceHolder) e.Item.FindControl("albumYearPlaceHolder");

        Debug.Assert(albumYearPlaceHolder != null);

        albumYearPlaceHolder.Visible = false;
    }

    private IEnumerable<AlbumYear> OrderChronologically(AlbumYear[] source)
    {
        return from albumYear in source
               orderby albumYear.Year descending
               select albumYear;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IImageEditorService imageEditorService = new ImageEditorService();
        IAlbumYearLoaderService albumYearLoaderService = new AlbumYearLoaderService("~/photo_albums/", imageEditorService);
        IAsyncCacheItemLoaderService asyncCacheItemLoaderService = new AsyncCacheItemLoaderService();
        ILoaderAsyncService<AlbumYear[]> photoAlbumLoaderService = new PhotoAlbumLoaderService(albumYearLoaderService, asyncCacheItemLoaderService);

        AlbumYear[] albumYears = photoAlbumLoaderService.GetLoadedObject();

        if (albumYears == null)
        {
            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.LoadingView;

            photoAlbumLoaderService.LoadAsync();

            AddNoCacheMetaTag();

            RegisterLoadingViewClientScriptIncludes();

            ShowProgressIndicator();
        }
        else
        {
            if (AlbumYearQueryStringValue == null && AlbumNameQueryStringValue == null)
            {
                photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.AlbumView;

                albumYearRepeater.DataSource = GetAlbumYearRepeaterDataSource(albumYears);
                albumYearRepeater.DataBind();
            }
            else
            {
                IPhotoAlbumFinderService photoAlbumFinderService = new PhotoAlbumFinderService(albumYears);

                if ((AlbumYearQueryStringValue != null && AlbumNameQueryStringValue == null))
                {
                    photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.AlbumView;

                    albumYearRepeater.DataSource = GetAlbumYearRepeaterDataSource((int) AlbumYearQueryStringValue, photoAlbumFinderService);
                    albumYearRepeater.DataBind();
                }
                else
                {
                    photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.PhotoView;

                    EnablePhotoAnimations();

                    Photo[] dataSource = GetPhotoRepeaterDataSource((int) AlbumYearQueryStringValue, AlbumNameQueryStringValue, photoAlbumFinderService);

                    if (dataSource.Length == 0)
                    {
                        photoViews.ActiveViewIndex = (int) PhotoView.NoPhotosView;
                    }
                    else
                    {
                        photoViews.ActiveViewIndex = (int) PhotoView.SomePhotosView;

                        photoRepeater.DataSource = dataSource;
                        photoRepeater.DataBind();
                    }
                }
            }
        }
    }

    private string PreventLightboxImageSetIdentifierBug(string identifier)
    {
        return identifier.Replace('\'', '_');
    }

    private void RegisterLightboxClientScriptIncludes()
    {
        ClientScript.RegisterClientScriptInclude("~/lightbox/js/prototype.js", ResolveUrl("~/lightbox/js/prototype.js"));
        ClientScript.RegisterClientScriptInclude("~/lightbox/js/scriptaculous.js?load=effects,builder",
                                                 ResolveUrl("~/lightbox/js/scriptaculous.js?load=effects,builder"));
        ClientScript.RegisterClientScriptInclude("~/lightbox/js/lightbox.js", ResolveUrl("~/lightbox/js/lightbox.js"));
        ClientScript.RegisterClientScriptInclude("~/lightbox/js/lightbox_config.js", ResolveUrl("~/lightbox/js/lightbox_config.js"));
    }

    private void RegisterLoadingViewClientScriptIncludes()
    {
        ClientScript.RegisterClientScriptInclude("~/photo_albums/default.aspx.js", ResolveUrl("~/photo_albums/default.aspx.js"));
    }

    private void ShowProgressIndicator()
    {
        progressIndicator.Visible = true;
    }

    #endregion

    #region Nested Type: PhotoAlbumView

    protected enum PhotoAlbumView
    {
        LoadingView = 0,
        AlbumView = 1,
        PhotoView = 2
    }

    #endregion

    #region Nested Type: PhotoView

    protected enum PhotoView
    {
        NoPhotosView = 0,
        SomePhotosView = 1
    }

    #endregion
}