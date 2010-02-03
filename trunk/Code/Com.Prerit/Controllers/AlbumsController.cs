using System.Web.Mvc;

namespace Com.Prerit.Controllers
{
    public partial class AlbumsController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult AlbumsByYear(int year)
        {
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult AlbumsByYearAndTitle(int year, string title)
        {
            return new EmptyResult();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Index()
        {
            return new EmptyResult();
        }

        #endregion
    }
}