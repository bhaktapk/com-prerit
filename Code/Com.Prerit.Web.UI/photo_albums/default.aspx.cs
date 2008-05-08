using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Com.Prerit.Web;

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

    private List<Photo> GetListOfPhotosByAlbumName()
    {
        return new List<Photo>()
        {
            new Photo("example1.jpg", "~/photo_albums/2007/our_first_house/example1.jpg", 112, 150),
            new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 150, 112),
            new Photo("example3.jpg", "~/photo_albums/2007/our_first_house/example3.jpg", 112, 150),
            new Photo("example3.jpg", "~/photo_albums/2007/our_first_house/example3.jpg", 112, 150),
            new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 150, 112),
            new Photo("example1.jpg", "~/photo_albums/2007/our_first_house/example1.jpg", 112, 150)
        }
        ;
    }

    private List<List<Album>> GetListsOfAlbumsOrderByAlbumYear()
    {
        List<List<Album>> albums;

        if (AlbumYearQueryStringValue == null)
        {
            albums = new List<List<Album>>()
            {
                {
                    new List<Album>
                    {
                        {
                            new Album("Our First House",
                                      2007,
                                      "~/photo_albums/2007/our_first_house/",
                                      new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 224, 168))
                        }
                    ,
                    }
                }
            ,
                {
                    new List<Album>
                    {
                        {
                            new Album("Christmas",
                                      2006,
                                      "~/photo_albums/2006/christmas/",
                                      new Photo("example1.jpg", "~/photo_albums/2006/christmas/example1.jpg", 168, 224))
                        }
                    ,
                        {
                            new Album("Our Honeymoon",
                                      2006,
                                      "~/photo_albums/2006/our_honeymoon/",
                                      new Photo("example2.jpg", "~/photo_albums/2006/our_honeymoon/example2.jpg", 224, 168))
                        }
                    ,
                        {
                            new Album("Our Wedding",
                                      2006,
                                      "~/photo_albums/2006/our_wedding/",
                                      new Photo("example3.jpg", "~/photo_albums/2006/our_wedding/example3.jpg", 168, 224))
                        }
                    ,
                        {
                            new Album("P's Pithi",
                                      2006,
                                      "~/photo_albums/2006/p's_pithi/",
                                      new Photo("example1.jpg", "~/photo_albums/2006/p's_pithi/example1.jpg", 168, 224))
                        }
                    ,
                    }
                }
            }
            ;
        }
        else
        {
            albums = new List<List<Album>>()
            {
                {
                    new List<Album>
                    {
                        {
                            new Album("Our First House",
                                      2007,
                                      "~/photo_albums/2007/our_first_house/",
                                      new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 224, 168))
                        }
                    ,
                    }
                }
            }
            ;
        }

        return albums;
    }

    private List<List<Album>> GetListsOfAlbumsOrderByAlbumYear(int value)
    {
        List<List<Album>> albums = new List<List<Album>>()
        {
            {
                new List<Album>
                {
                    {
                        new Album("Our First House",
                                  2007,
                                  "~/photo_albums/2007/our_first_house/",
                                  new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 224, 168))
                    }
                ,
                }
            }
        }
        ;

        return albums;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (AlbumYearQueryStringValue == null && AlbumNameQueryStringValue == null)
        {
            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.AlbumView;

            albumYearRepeater.DataSource = GetListsOfAlbumsOrderByAlbumYear();
            albumYearRepeater.DataBind();
        }
        else if ((AlbumYearQueryStringValue != null && AlbumNameQueryStringValue == null))
        {
            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.AlbumView;

            albumYearRepeater.DataSource = GetListsOfAlbumsOrderByAlbumYear();
            albumYearRepeater.DataBind();
        }
        else
        {
            EnablePhotoAnimations();

            photoAlbumViews.ActiveViewIndex = (int) PhotoAlbumView.PhotoView;

            photoRepeater.DataSource = GetListOfPhotosByAlbumName();
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