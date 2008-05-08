using System.Collections.Generic;

namespace Com.Prerit.Web.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        #region Methods

        public List<Photo> GetListOfPhotosByAlbumName()
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

        public List<List<Album>> GetListsOfAlbumsOrderByAlbumYear()
        {
            return new List<List<Album>>()
            {
                new List<Album>
                {
                    new Album("Our First House",
                              2007,
                              "~/photo_albums/2007/our_first_house/",
                              new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 224, 168))
                    ,
                }
            ,
                new List<Album>
                {
                    new Album("Christmas",
                              2006,
                              "~/photo_albums/2006/christmas/",
                              new Photo("example1.jpg", "~/photo_albums/2006/christmas/example1.jpg", 168, 224))
                    ,
                    new Album("Our Honeymoon",
                              2006,
                              "~/photo_albums/2006/our_honeymoon/",
                              new Photo("example2.jpg", "~/photo_albums/2006/our_honeymoon/example2.jpg", 224, 168))
                    ,
                    new Album("Our Wedding",
                              2006,
                              "~/photo_albums/2006/our_wedding/",
                              new Photo("example3.jpg", "~/photo_albums/2006/our_wedding/example3.jpg", 168, 224))
                    ,
                    new Album("P's Pithi",
                              2006,
                              "~/photo_albums/2006/p's_pithi/",
                              new Photo("example1.jpg", "~/photo_albums/2006/p's_pithi/example1.jpg", 168, 224))
                    ,
                }
            }
            ;
        }

        public List<List<Album>> GetListsOfAlbumsOrderByAlbumYear(int value)
        {
            return new List<List<Album>>()
            {
                new List<Album>
                {
                    new Album("Our First House",
                              2007,
                              "~/photo_albums/2007/our_first_house/",
                              new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 224, 168))
                }
            }
            ;
        }

        #endregion
    }
}