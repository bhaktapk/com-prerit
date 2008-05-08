using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

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

    private void EnablePhotoAnimations()
    {
        AddLightboxCss();
        RegisterLightboxClientScriptIncludes();
    }

    public string GetAlbumNameAsTitle()
    {
        string albumNameAsTitle = null;

        if (Request.QueryString[albumNameQueryStringKey] != null)
        {
            string[] words = Request.QueryString[albumNameQueryStringKey].Split('_');

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

    protected string GetLightboxIdentifier()
    {
        return string.Format("lightbox[{0}]", AlbumNameQueryStringValue);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IPhotoAlbumService photoAlbumService = new PhotoAlbumService();

        if (AlbumYearQueryStringValue == null && AlbumNameQueryStringValue == null)
        {
            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.AlbumView;

            albumYearRepeater.DataSource = photoAlbumService.GetAlbumsGroupedByAlbumYear();
            albumYearRepeater.DataBind();
        }
        else if ((AlbumYearQueryStringValue != null && AlbumNameQueryStringValue == null))
        {
            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.AlbumView;

            albumYearRepeater.DataSource = photoAlbumService.GetAlbumsByAlbumYearGroupedByAlbumYear((int) AlbumYearQueryStringValue);
            albumYearRepeater.DataBind();
        }
        else
        {
            EnablePhotoAnimations();

            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.PhotoView;

            photoRepeater.DataSource = photoAlbumService.GetPhotosByAlbumName(AlbumNameQueryStringValue);
            photoRepeater.DataBind();
        }
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