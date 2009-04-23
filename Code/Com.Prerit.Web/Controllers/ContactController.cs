using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using Com.Prerit.Domain;
using Com.Prerit.Services;
using Com.Prerit.Web.Filters;
using Com.Prerit.Web.Models.Contact;

using MvcContrib.Filters;

namespace Com.Prerit.Web.Controllers
{
    public class ContactController : DefaultMasterController
    {
        #region Fields

        private readonly IEmailSenderService _emailSenderService;

        #endregion

        #region Constructors

        public ContactController()
            : this(new EmailSenderService(EmailInfo.SmtpHost))
        {
        }

        private ContactController(IEmailSenderService emailSenderService)
        {
            if (emailSenderService == null)
            {
                throw new ArgumentNullException("emailSender");
            }

            _emailSenderService = emailSenderService;
        }

        #endregion

        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName(Action.EmailSent)]
        [TempDataToModel]
        public ActionResult EmailSent()
        {
            var indexModel = ViewData.Model as IndexModel;

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
        [ModelToTempData]
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

            var email = new Email
                            {
                                FromEmailAddress = EmailInfo.AuthorEmailAddress,
                                ToEmailAddress = model.EmailAddress,
                                Subject = EmailInfo.GetContactEmailSubject(model.Name),
                                Message = model.Message,
                            };

            _emailSenderService.Send(email);

            return RedirectToActionWithModel(Action.EmailSent, model);
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