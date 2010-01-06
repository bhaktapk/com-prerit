using System;
using System.Globalization;
using System.Web.Mvc;

using Com.Prerit.Models.Shared;

namespace Com.Prerit.Controllers
{
    public partial class DefaultMasterController : Controller
    {
        #region Methods

        [ActionName(ActionName.ContentEncoding)]
        public virtual ActionResult ContentEncoding()
        {
            var model = new ContentEncodingModel { ContentEncoding = Response.ContentEncoding.WebName };

            return View(model);
        }

        [ActionName(ActionName.ContentType)]
        public virtual ActionResult ContentType()
        {
            var model = new ContentTypeModel { ContentType = Response.ContentType };

            return View(model);
        }

        [ActionName(ActionName.Culture)]
        public virtual ActionResult Culture()
        {
            var model = new CultureModel { Culture = CultureInfo.CurrentCulture.Name.ToLowerInvariant() };

            return View(model);
        }

        [ActionName(ActionName.CurrentUrlEncoded)]
        public virtual ActionResult CurrentUrlEncoded()
        {
            var model = new CurrentUrlEncodedModel { CurrentUrlEncoded = Request.Url.AbsoluteUri };

            return View(model);
        }

        [ActionName(ActionName.Year)]
        public virtual ActionResult Year()
        {
            var model = new YearModel { Year = DateTime.Today.Year };

            return View(model);
        }

        #endregion

        #region Nested Type: ActionName

        public static class ActionName
        {
            #region Constants

            public const string ContentEncoding = "Content-Encoding";

            public const string ContentType = "Content-Type";

            public const string Culture = "Culture";

            public const string CurrentUrlEncoded = "Current-Url-Encoded";

            public const string Year = "Year";

            #endregion
        }

        #endregion
    }
}