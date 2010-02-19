using System;
using System.Globalization;
using System.Web.Mvc;

namespace Com.Prerit.Controllers
{
    public partial class DefaultMasterController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult ContentEncoding()
        {
            return new ContentResult
                       {
                           Content = Response.ContentEncoding.WebName
                       };
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult ContentType()
        {
            return new ContentResult
                       {
                           Content = Response.ContentType
                       };
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Culture()
        {
            return new ContentResult
                       {
                           Content = CultureInfo.CurrentCulture.Name.ToLower()
                       };
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult CurrentUrlEncoded()
        {
            return new ContentResult
                       {
                           Content = Url.Encode(Request.Url.AbsoluteUri)
                       };
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Year()
        {
            return new ContentResult
                       {
                           Content = DateTime.Today.Year.ToString()
                       };
        }

        #endregion
    }
}