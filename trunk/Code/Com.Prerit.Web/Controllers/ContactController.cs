using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using Com.Prerit.Web.Models.Contact;

using MvcContrib.Filters;

namespace Com.Prerit.Web.Controllers
{
    public class ContactController : DefaultMasterController
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName(Action.EmailSent)]
        public ActionResult EmailSent()
        {
            var indexModel = GetTempModel<IndexModel>();

            if (indexModel == null)
            {
                return RedirectToAction(Action.Index);
            }

            EmailSentModel model = UpdateModelBase(new EmailSentModel(indexModel));

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName(Action.Index)]
        [ModelStateToTempData]
        public ActionResult Index()
        {
            IndexModel model = UpdateModelBase(new IndexModel());

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName(Action.SendEmail)]
        [ModelStateToTempData]
        public ActionResult SendEmail(IndexModel model)
        {
            if (model.Name.Trim().Length == 0)
            {
                ModelState.AddModelError(IndexModel.PropertyName.Name, "Name is required");
            }

            if (model.EmailAddress.Trim().Length == 0)
            {
                ModelState.AddModelError(IndexModel.PropertyName.EmailAddress, "E-mail is required");
            }
            else if (!Regex.IsMatch(model.EmailAddress, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                ModelState.AddModelError(IndexModel.PropertyName.EmailAddress, "E-mail is not in a correct format");
            }

            if (model.Message.Trim().Length == 0)
            {
                ModelState.AddModelError(IndexModel.PropertyName.Message, "Message is required");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(Action.Index);
            }

            var smtpClient = new SmtpClient
                                 {
                                     Host = WebsiteInfo.SmtpHost
                                 };

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(WebsiteInfo.AuthorEmailAddress);
                message.To.Add(model.EmailAddress);
                message.Subject = WebsiteInfo.GetContactEmailSubject(model.Name);
                message.Body = model.Message;
                message.IsBodyHtml = false;

                //smtpClient.Send(message);
            }

            SetTempModel(model);

            return RedirectToAction(Action.EmailSent);
        }

        #endregion

        #region Nested Type: Action

        public static class Action
        {
            #region Constants

            public const string EmailSent = "email-sent";

            public const string Index = SharedAction.Index;

            public const string SendEmail = "send-email";

            #endregion
        }

        #endregion

        #region Nested Type: Name

        public static class Name
        {
            #region Fields

            public static readonly string Seo = GetSeoControllerName<ContactController>();

            public static readonly string WithoutSuffix = GetControllerNameWithoutSuffix<ContactController>();

            #endregion
        }

        #endregion
    }
}