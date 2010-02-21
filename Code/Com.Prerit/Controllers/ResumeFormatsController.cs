using System.Web;
using System.Web.Mvc;

using Com.Prerit.ActionResults;

using Links;

namespace Com.Prerit.Controllers
{
    public partial class ResumeFormatsController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult AdobePdf()
        {
            return new StaticFilePathResult(App_Data.ResumeFormats.AdobePdf_pdf, "application/pdf", HttpCacheability.Public)
                       {
                           FileDownloadName = "Resume of Prerit Bhakta.pdf"
                       };
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult MicrosoftWord()
        {
            return new StaticFilePathResult(App_Data.ResumeFormats.MicrosoftWord_doc, "application/msword", HttpCacheability.Public)
                       {
                           FileDownloadName = "Resume of Prerit Bhakta.doc"
                       };
        }

        #endregion
    }
}