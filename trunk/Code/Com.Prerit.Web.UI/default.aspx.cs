using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Com.Prerit.Web.UI
{
    public class _Default : Page
    {
        #region Methods

        public void Page_Load(object sender, EventArgs e)
        {
            string originalPath = Request.Path;
            HttpContext.Current.RewritePath(Request.ApplicationPath, false);
            IHttpHandler httpHandler = new MvcHttpHandler();
            httpHandler.ProcessRequest(HttpContext.Current);
            HttpContext.Current.RewritePath(originalPath, false);
        }

        #endregion
    }
}