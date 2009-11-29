using System;
using System.Globalization;
using System.Web.Mvc;

using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Controllers
{
    public abstract class DefaultMasterController : ApplicationController<DefaultMasterModel>
    {
        #region Methods

        [ActionName(Action.MetaTags)]
        public ActionResult MetaTags()
        {
            var model = new MetaTagsModel
                            {
                                ContentEncoding = Response.ContentEncoding.WebName,
                                ContentType = Response.ContentType,
                                Culture = CultureInfo.CurrentCulture.Name.ToLowerInvariant(),
                                CurrentYear = DateTime.Today.Year
                            };

            return View(model);
        }

        public override T UpdateModelBase<T>(T model)
        {
            model.CopyrightBeginYear = WebsiteInfo.DomainRegistrationYear;
            model.CopyrightEndYear = DateTime.Today.Year;
            model.SiteName = WebsiteInfo.SiteName;
            model.ValidationUri = Request.Url.AbsoluteUri;

            return model;
        }

        #endregion

        #region Nested Type: Action

        public static class Action
        {
            #region Constants

            public const string MetaTags = "meta-tags";

            #endregion
        }

        #endregion
    }
}