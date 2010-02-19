using System.Web.Mvc;

using Links;

namespace Com.Prerit.Controllers
{
    public partial class ResumeFormatsController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult AdobePdf()
        {
            return new FilePathResult(App_Data.ResumeFormats.AdobePdf_pdf, "application/pdf")
                       {
                           FileDownloadName = "Resume of Prerit Bhakta.pdf"
                       };
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult MicrosoftWord()
        {
            return new FilePathResult(App_Data.ResumeFormats.MicrosoftWord_doc, "application/msword")
                       {
                           FileDownloadName = "Resume of Prerit Bhakta.doc"
                       };
        }

        #endregion
    }
}