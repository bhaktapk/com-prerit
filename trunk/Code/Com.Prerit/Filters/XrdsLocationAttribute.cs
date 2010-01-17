﻿using System.Web.Mvc;

namespace Com.Prerit.Filters
{
    public class XrdsLocationAttribute : ActionFilterAttribute
    {
        #region Methods

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);

            filterContext.HttpContext.Response.AppendHeader("x-xrds-location", urlHelper.Action(MVC.OpenId.Xrds()));

            base.OnResultExecuted(filterContext);
        }

        #endregion
    }
}