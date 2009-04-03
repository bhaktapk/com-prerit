using System;
using System.Web.Mvc;

using Com.Prerit.Web.UI.Views.Shared;

namespace Com.Prerit.Web.UI.Controllers
{
    public abstract class DefaultMasterController<T> : Controller where T : DefaultMasterModel, new()
    {
        #region Methods

        public T CreateBaseModel()
        {
            {
                return new T
                           {
                               CopyrightBeginYear = WebsiteInfo.DomainRegistrationYear,
                               CopyrightEndYear = DateTime.Today.Year,
                               SiteName = WebsiteInfo.SiteName,
                               ValidationUri = Request.Url,
                               WebsiteAuthor = WebsiteInfo.Author
                           };
            }
        }

        #endregion
    }
}