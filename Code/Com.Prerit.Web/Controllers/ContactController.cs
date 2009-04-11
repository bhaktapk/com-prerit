using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using Com.Prerit.Web.Models.Contact;

namespace Com.Prerit.Web.Controllers
{
    public class ContactController : DefaultMasterController
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName(Action.Index)]
        public ActionResult Index()
        {
            IndexModel model = UpdateModelBase(new IndexModel());

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName(Action.Index)]
        public ActionResult Index(IndexModel model)
        {
            UpdateModelBase(model);

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

            if (ModelState.IsValid)
            {
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

                    smtpClient.Send(message);
                }
            }

            return View(model);
        }

        #endregion

        #region Nested Type: Action

        public static class Action
        {
            #region Constants

            public const string Index = SharedAction.Index;

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