using System;
using System.Collections.Generic;
using System.Web.UI;

using Com.Prerit.Web;

public partial class photos_default : Page
{
    #region Methods

    private List<List<Album>> GetListsOfAlbumsOrderByAlbumYear()
    {
        List<List<Album>> albums = new List<List<Album>>()
        {
            { new List<Album>
                {
                    { new Album("Our First Home", 2007, "~/photos/albums/2007/our_first_house/", new Photo("example2.jpg", "~/photos/albums/2007/our_first_house/example2.jpg", 250, 187)) },
                }
            },
            { new List<Album>
                {
                    { new Album("Christmas", 2006, "~/photos/albums/2006/christmas/", new Photo("example1.jpg", "~/photos/albums/2006/christmas/example1.jpg", 187, 250)) },
                    { new Album("Our Honeymoon", 2006, "~/photos/albums/2006/our_honeymoon/", new Photo("example2.jpg", "~/photos/albums/2006/our_honeymoon/example2.jpg", 250, 187)) },
                    { new Album("Our Wedding", 2006, "~/photos/albums/2006/our_wedding/", new Photo("example3.jpg", "~/photos/albums/2006/our_wedding/example3.jpg", 187, 250)) },
                    { new Album("P's Pithi", 2006, "~/photos/albums/2006/p's_pithi/", new Photo("example1.jpg", "~/photos/albums/2006/p's_pithi/example1.jpg", 187, 250)) },
                }
            }
        };

        return albums;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        albumYearRepeater.DataSource = GetListsOfAlbumsOrderByAlbumYear();
        albumYearRepeater.DataBind();
    }

    #endregion
}