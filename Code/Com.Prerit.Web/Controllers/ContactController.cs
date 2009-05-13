using System;
using System.Web.Mvc;

using Castle.Components.Validator;

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
            : this(new EmailSenderService(EmailInfo.SmtpHost, new ValidatorRunner(new CachedValidationRegistry())))
        {
        }

        public ContactController(IEmailSenderService emailSenderService)
        {
            if (emailSenderService == null)
            {
                throw new ArgumentNullException("emailSenderService");
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
            if (ModelState.IsValid)
            {
                var email = new Email
                                {
                                    FromEmailAddress = EmailInfo.AuthorEmailAddress,
                                    ToEmailAddress = model.EmailAddress,
                                    Subject = EmailInfo.GetContactEmailSubject(model.Name),
                                    Message = model.Message,
                                };

                if (_emailSenderService.IsEmailValidToSend(email))
                {
                    _emailSenderService.Send(email);
                }
                else
                {
                    ModelState.AddModelErrors(_emailSenderService.GetErrorSummaryForInvalidEmail(email));
                }
            }

            return ModelState.IsValid ? RedirectToActionWithModel(Action.EmailSent, model) : RedirectToAction(Action.Index);
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