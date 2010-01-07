using System;
using System.Globalization;
using System.Web.Mvc;

using Com.Prerit.Models.Shared;

namespace Com.Prerit.Controllers
{
    public partial class DefaultMasterController : Controller
    {
        #region Methods

        public virtual ActionResult ContentEncoding()
        {
            var model = new ContentEncodingModel { ContentEncoding = Response.ContentEncoding.WebName };

            return View(model);
        }

        public virtual ActionResult ContentType()
        {
            var model = new ContentTypeModel { ContentType = Response.ContentType };

            return View(model);
        }

        public virtual ActionResult Culture()
        {
            var model = new CultureModel { Culture = CultureInfo.CurrentCulture.Name.ToLowerInvariant() };

            return View(model);
        }

        public virtual ActionResult CurrentUrlEncoded()
        {
            var model = new CurrentUrlEncodedModel { CurrentUrlEncoded = Request.Url.AbsoluteUri };

            return View(model);
        }

        public virtual ActionResult Year()
        {
            var model = new YearModel { Year = DateTime.Today.Year };

            return View(model);
        }

        #endregion
    }
}