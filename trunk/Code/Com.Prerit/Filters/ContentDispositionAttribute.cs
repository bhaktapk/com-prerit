using System;
using System.Web.Mvc;

namespace Com.Prerit.Filters
{
    public class ContentDispositionAttribute : ActionFilterAttribute
    {
        private readonly string _filename;

        public ContentDispositionAttribute(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }

            _filename = filename;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader("content-disposition", string.Format("attachment; filename=\"{0}\"", _filename));

            base.OnResultExecuted(filterContext);
        }
    }
}