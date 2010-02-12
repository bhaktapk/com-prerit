using System.Web.Mvc;

using Com.Prerit.Domain;
using Com.Prerit.Filters;

namespace Com.Prerit.Controllers
{
    public partial class AlbumsController : Controller
    {
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
            return new EmptyResult();
        }

        #endregion
    }
}