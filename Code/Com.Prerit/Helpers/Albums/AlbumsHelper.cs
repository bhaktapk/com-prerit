using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using Com.Prerit.Domain;
using Com.Prerit.Models.Albums;

namespace Com.Prerit.Helpers.Albums
{
    public static class AlbumsHelper
    {
        #region Methods

        public static void RenderAlbumPortraitPartial(this HtmlHelper<AlbumsByYearModel> helper)
        {
            if (helper == null)
            {
                throw new ArgumentNullException("helper");
            }

            foreach (Album album in helper.ViewData.Model.Albums)
            {
                var model = new AlbumPortraitModel
                                {
                                    Album = album
                                };

                helper.RenderPartial(MVC.Albums.Views.AlbumPortrait, model);
            }
        }

        public static void RenderAlbumsByYearPartial(this HtmlHelper<AllAlbumsModel> helper)
        {
            if (helper == null)
            {
                throw new ArgumentNullException("helper");
            }

            foreach (var groupedAlbum in helper.ViewData.Model.GroupedAlbums)
            {
                var model = new AlbumsByYearModel
                                {
                                    Albums = groupedAlbum,
                                    Year = groupedAlbum.Key
                                };

                helper.RenderPartial(MVC.Albums.Views.AlbumsByYear, model);
            }
        }

        public static void RenderAlbumsByYearPartial(this HtmlHelper<AlbumsByYearModel> helper)
        {
            if (helper == null)
            {
                throw new ArgumentNullException("helper");
            }

            helper.RenderPartial(MVC.Albums.Views.AlbumsByYear);
        }

        #endregion
    }
}