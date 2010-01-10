using System.Web.Mvc;

using Com.Prerit.Filters;

using Links;

namespace Com.Prerit.Controllers
{
    public partial class ResumeFormatsController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ContentDisposition("Resume of Prerit Bhakta.pdf")]
        public virtual ActionResult AdobePdf()
        {
            return new FilePathResult(App_Data.ResumeFormats.AdobePdf_pdf, "application/pdf");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ContentDisposition("Resume of Prerit Bhakta.doc")]
        public virtual ActionResult MicrosoftWord()
        {
            return new FilePathResult(App_Data.ResumeFormats.MicrosoftWord_doc, "application/msword");
        }

        #endregion
    }
}