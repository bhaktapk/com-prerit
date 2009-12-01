using System;
using System.Globalization;
using System.Web.Mvc;

using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Controllers
{
    public abstract class DefaultMasterController : ApplicationController
    {
        #region Methods

        [ActionName(Action.ContentEncoding)]
        public ActionResult ContentEncoding()
        {
            var model = new ContentEncodingModel { ContentEncoding = Response.ContentEncoding.WebName };

            return View(model);
        }

        [ActionName(Action.ContentType)]
        public ActionResult ContentType()
        {
            var model = new ContentTypeModel { ContentType = Response.ContentType };

            return View(model);
        }

        [ActionName(Action.Culture)]
        public ActionResult Culture()
        {
            var model = new CultureModel { Culture = CultureInfo.CurrentCulture.Name.ToLowerInvariant() };

            return View(model);
        }

        [ActionName(Action.CurrentUrlEncoded)]
        public ActionResult CurrentUrlEncoded()
        {
            var model = new CurrentUrlEncodedModel { CurrentUrlEncoded = Request.Url.AbsoluteUri };

            return View(model);
        }

        [ActionName(Action.Year)]
        public ActionResult Year()
        {
            var model = new YearModel { Year = DateTime.Today.Year };

            return View(model);
        }

        #endregion

        #region Nested Type: Action

        public static class Action
        {
            #region Constants

            public const string ContentEncoding = "content-encoding";

            public const string ContentType = "content-type";

            public const string Culture = "culture";

            public const string CurrentUrlEncoded = "current-url-encoded";

            public const string Year = "year";

            #endregion
        }

        #endregion
    }
}