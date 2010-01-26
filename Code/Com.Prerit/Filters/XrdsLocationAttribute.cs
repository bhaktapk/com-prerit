using System;
using System.Web.Mvc;

namespace Com.Prerit.Filters
{
    public class XrdsLocationAttribute : ActionFilterAttribute
    {
        #region Methods

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);

            string xrdsLocationUrl = new Uri(filterContext.HttpContext.Request.Url, urlHelper.Action(MVC.OpenId.Xrds())).AbsoluteUri;

            filterContext.HttpContext.Response.AppendHeader("X-XRDS-Location", xrdsLocationUrl);

            base.OnResultExecuted(filterContext);
        }

        #endregion
    }
}