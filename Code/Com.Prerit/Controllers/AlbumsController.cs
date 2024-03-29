using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Com.Prerit.ActionResults;
using Com.Prerit.Domain;
using Com.Prerit.Filters;
using Com.Prerit.Models.Albums;
using Com.Prerit.Services;

namespace Com.Prerit.Controllers
{
    public partial class AlbumsController : Controller
    {
        #region Fields

        private readonly IAlbumService _albumService;

        #endregion

        #region Constructors

        public AlbumsController(IAlbumService albumService)
        {
            if (albumService == null)
            {
                throw new ArgumentNullException("albumService");
            }

            _albumService = albumService;
        }

        #endregion

        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoleTypes = RoleType.Admin)]
        public virtual ActionResult AlbumByYearAndSlug(int year, string slug)
        {
            Album album = _albumService.GetAlbum(year, slug);

            var model = new AlbumByYearAndSlugModel
                            {
                                Album = album
                            };

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoleTypes = RoleType.Admin)]
        public virtual ActionResult AlbumsByYear(int year)
        {
            IEnumerable<Album> albums = _albumService.GetAlbums(year);

            var model = new AlbumsByYearModel
                            {
                                Albums = albums,
                                Year = year
                            };

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoleTypes = RoleType.Admin)]
        public virtual ActionResult AllAlbums()
        {
            IEnumerable<Album> albums = _albumService.GetAlbums();

            if (albums == null || albums.Count() == 0)
            {
                return View(MVC.Albums.Views.NoAlbums);
            }

            var model = new AllAlbumsModel
                            {
                                GroupedAlbums = albums.OrderByDescending(album => album.Year).GroupBy(album => album.Year)
                            };

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoleTypes = RoleType.Admin)]
        public virtual ActionResult Portrait(int year, string slug)
        {
            WebImage webImage = _albumService.GetAlbumPortrait(year, slug);

            return new StaticFilePathResult(webImage.FilePath, "image/jpeg", HttpCacheability.ServerAndPrivate);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoleTypes = RoleType.Admin)]
        public virtual ActionResult Thumbnail(int year, string slug, int photoItem)
        {
            int photoIndex = photoItem - 1;

            WebImage webImage = _albumService.GetAlbumPhoto(year, slug, photoIndex, AlbumPhotoType.Thumbnail);

            return new StaticFilePathResult(webImage.FilePath, "image/jpeg", HttpCacheability.ServerAndPrivate);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoleTypes = RoleType.Admin)]
        public virtual ActionResult WebOptimized(int year, string slug, int photoItem)
        {
            int photoIndex = photoItem - 1;

            WebImage webImage = _albumService.GetAlbumPhoto(year, slug, photoIndex, AlbumPhotoType.WebOptimized);

            return new StaticFilePathResult(webImage.FilePath, "image/jpeg", HttpCacheability.ServerAndPrivate);
        }

        #endregion
    }
}