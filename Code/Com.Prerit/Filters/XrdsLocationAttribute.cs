using System;
using System.Web.Mvc;

namespace Com.Prerit.Filters
{
    public class XrdsLocationAttribute : ActionFilterAttribute
    {
        #region Fields

        private readonly string _path;

        #endregion

        #region Constructors

        public XrdsLocationAttribute(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            _path = path;
        }

        #endregion

        #region Methods

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader("x-xrds-location", _path);

            base.OnResultExecuted(filterContext);
        }

        #endregion
    }
}