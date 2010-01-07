using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Com.Prerit
{
    public class _Default : Page
    {
        #region Methods

        public void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.RewritePath(Request.ApplicationPath, false);
            ((IHttpHandler) new MvcHttpHandler()).ProcessRequest(HttpContext.Current);
            HttpContext.Current.RewritePath(Request.Path, false);
        }

        #endregion
    }
}