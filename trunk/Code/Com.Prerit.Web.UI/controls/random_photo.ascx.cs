using System;
using System.Web.UI;

using Com.Prerit.Domain;
using Com.Prerit.Services;

public partial class controls_random_photo : UserControl
{
    #region Methods

    private Album GetRandomAlbum(Album[] albums, Random random)
    {
        return albums[random.Next() % albums.Length];
    }

    private AlbumYear GetRandomAlbumYear(AlbumYear[] albumYears, Random random)
    {
        return albumYears[random.Next() % albumYears.Length];
    }

    private Photo GetRandomPhoto(Photo[] photos, Random random)
    {
        return photos[random.Next() % photos.Length];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IImageEditorService imageEditorService = new ImageEditorService();
        IAlbumYearLoaderService albumYearLoaderService = new AlbumYearLoaderService("~/photo_albums/", imageEditorService);
        IAsyncCacheItemLoaderService asyncCacheItemLoaderService = new AsyncCacheItemLoaderService();
        IAsyncLoaderService<AlbumYear[]> photoAlbumLoaderService = new PhotoAlbumLoaderService(albumYearLoaderService, asyncCacheItemLoaderService);

        Visible = false;

        LoaderAsyncServiceStatus status = photoAlbumLoaderService.Status;

        switch (status)
        {
            case LoaderAsyncServiceStatus.Idle:
                photoAlbumLoaderService.LoadAsync();
                break;
            case LoaderAsyncServiceStatus.Loading:
                break;
            case LoaderAsyncServiceStatus.FailedLoad:
                break;
            case LoaderAsyncServiceStatus.Completed:
                AlbumYear[] albumYears = photoAlbumLoaderService.LoadedObject;

                if (albumYears != null && albumYears.Length != 0)
                {
                    Random random = new Random();

                    AlbumYear albumYear = GetRandomAlbumYear(albumYears, random);

                    if (albumYear.Albums != null && albumYear.Albums.Length != 0)
                    {
                        Album album = GetRandomAlbum(albumYear.Albums, random);

                        if (album.Photos != null && album.Photos.Length != 0)
                        {
                            Photo photo = GetRandomPhoto(album.Photos, random);

                            photoAlbumLink.HRef = album.VirtualPath;
                            photoAlbumLink.Title = string.Format(photoAlbumLink.Title, album.AlbumName, albumYear.Year);
                            photoImage.Alt = string.Format(photoImage.Alt, album.AlbumName, albumYear.Year);
                            photoImage.Src = photo.Thumbnail.VirtualPath;

                            Visible = true;
                        }
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(string.Format("Unknown LoaderAsyncServiceStatus {0}", status));
        }
    }

    #endregion
}