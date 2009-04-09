using System;

using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Controllers
{
    public abstract class DefaultMasterController : ApplicationController<DefaultMasterModel>
    {
        #region Methods

        public override T UpdateModelBase<T>(T model)
        {
            model.ContentEncoding = Response.ContentEncoding.WebName;
            model.ContentType = Response.ContentType;
            model.CopyrightBeginYear = WebsiteInfo.DomainRegistrationYear;
            model.CopyrightEndYear = DateTime.Today.Year;
            model.SiteName = WebsiteInfo.SiteName;
            model.ValidationUri = Request.Url;

            return model;
        }

        #endregion
    }
}