using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
        public virtual ActionResult AlbumsByYear(int year)
        {
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoleTypes = RoleType.Admin)]
        public virtual ActionResult AlbumsByYearAndSlug(int year, string slug)
        {
            return new EmptyResult();
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

        #endregion
    }
}