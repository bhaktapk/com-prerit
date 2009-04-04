using System;
using System.Web.Mvc;

using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Controllers
{
    public abstract class DefaultMasterController : Controller
    {
        #region Methods

        public T CreateBaseModel<T>() where T : DefaultMasterModel, new()
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