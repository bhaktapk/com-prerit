using System.Web.Mvc;

using Com.Prerit.Domain;
using Com.Prerit.Filters;

namespace Com.Prerit.Controllers
{
    public partial class AlbumsController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoles = KnownRole.Admin)]
        public virtual ActionResult AlbumsByYear(int year)
        {
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoles = KnownRole.Admin)]
        public virtual ActionResult AlbumsByYearAndTitle(int year, string title)
        {
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [CustomAuthorize(AllowedRoles = KnownRole.Admin)]
        public virtual ActionResult Index()
        {
            return new EmptyResult();
        }

        #endregion
    }
}