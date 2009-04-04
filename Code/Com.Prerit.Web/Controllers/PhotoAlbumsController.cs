using System.Web.Mvc;

using Com.Prerit.Web.Models.PhotoAlbums;

namespace Com.Prerit.Web.Controllers
{
    public class PhotoAlbumsController : DefaultMasterController
    {
        #region Methods

        public ActionResult Index()
        {
            var model = CreateBaseModel<IndexModel>();

            return View(model);
        }

        #endregion
    }
}