using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Com.Prerit.Web;
using Com.Prerit.Web.Services;

public partial class photo_albums_default : Page
{
    #region Constants

    private const string albumNameQueryStringKey = "album_name";

    private const string albumYearQueryStringKey = "album_year";

    #endregion

    #region Properties

    public string AlbumNameQueryStringValue
    {
        get { return Request.QueryString[albumNameQueryStringKey]; }
    }

    public int? AlbumYearQueryStringValue
    {
        get
        {
            int parsedAlbumYear;

            if (int.TryParse(Request.QueryString[albumYearQueryStringKey], out parsedAlbumYear))
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

    private IEnumerable<KeyValuePair<int, Album[]>> GetAlbumYearRepeaterDataSource(int albumYear, IPhotoAlbumService photoAlbumService)
    {
        return MakeSortedListReverseChronological(photoAlbumService.GetAlbumsByAlbumYearGroupedByAlbumYear(albumYear));
    }

    private IEnumerable<KeyValuePair<int, Album[]>> GetAlbumYearRepeaterDataSource(IPhotoAlbumService photoAlbumService)
    {
        SortedList<int, Album[]> albumsSortedByYear = photoAlbumService.GetAlbumsGroupedByAlbumYear();

        IEnumerable<KeyValuePair<int, Album[]>> albumsSortedInChronologicalOrder = MakeSortedListReverseChronological(albumsSortedByYear);

        return albumsSortedInChronologicalOrder;
    }

    protected string GetLightboxImageSetIdentifier()
    {
        return PreventLightboxImageSetIdentifierBug(AlbumNameQueryStringValue);
    }

    private Photo[] GetPhotoRepeaterDataSource(int albumYear, string albumName, IPhotoAlbumService photoAlbumService)
    {
        return photoAlbumService.GetPhotosByAlbumYearAndAlbumName(albumYear, albumName);
    }

    private IEnumerable<KeyValuePair<int, Album[]>> MakeSortedListReverseChronological(SortedList<int, Album[]> source)
    {
        return source.Reverse();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IPhotoAlbumLoaderService photoAlbumLoaderService = new PhotoAlbumLoaderService("~/photo_albums/");
        IPhotoAlbumService photoAlbumService = new PhotoAlbumService(photoAlbumLoaderService);

        if (AlbumYearQueryStringValue == null && AlbumNameQueryStringValue == null)
        {
            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.AlbumView;

            albumYearRepeater.DataSource = GetAlbumYearRepeaterDataSource(photoAlbumService);
            albumYearRepeater.DataBind();
        }
        else if ((AlbumYearQueryStringValue != null && AlbumNameQueryStringValue == null))
        {
            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.AlbumView;

            albumYearRepeater.DataSource = GetAlbumYearRepeaterDataSource((int) AlbumYearQueryStringValue, photoAlbumService);
            albumYearRepeater.DataBind();
        }
        else
        {
            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.PhotoView;

            EnablePhotoAnimations();

            photoRepeater.DataSource = GetPhotoRepeaterDataSource((int) AlbumYearQueryStringValue, AlbumNameQueryStringValue, photoAlbumService);
            photoRepeater.DataBind();
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

    #endregion

    #region Nested Type: PhotoAlbumView

    protected enum PhotoAlbumView
    {
        AlbumView = 0,
        PhotoView = 1
    }

    #endregion
}