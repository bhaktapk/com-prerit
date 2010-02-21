using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Com.Prerit.ActionResults
{
    public class StaticFilePathResult : FilePathResult
    {
        #region Properties

        public HttpCacheability Cacheability { get; private set; }

        #endregion

        #region Constructors

        public StaticFilePathResult(string fileName, string contentType, HttpCacheability cacheability)
            : base(fileName, contentType)
        {
            Cacheability = cacheability;
        }

        #endregion

        #region Methods

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            string filePath = FileName.StartsWith("/") || FileName.StartsWith("~/") ? context.HttpContext.Server.MapPath(FileName) : FileName;

            context.HttpContext.Response.AppendHeader("Accept-Ranges", "bytes");
            context.HttpContext.Response.AddFileDependency(filePath);
            context.HttpContext.Response.Cache.SetLastModified(File.GetLastWriteTime(filePath));
            context.HttpContext.Response.Cache.SetETagFromFileDependencies();
            context.HttpContext.Response.Cache.SetCacheability(Cacheability);
            context.HttpContext.Response.Cache.SetOmitVaryStar(true);
        }

        #endregion
    }
}