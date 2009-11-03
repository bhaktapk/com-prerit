using System;
using System.Web.Mvc;

using AutoMapper;

using Castle.Components.Validator;

using Com.Prerit.Domain;
using Com.Prerit.Services;
using Com.Prerit.Web.Filters;
using Com.Prerit.Web.Infrastructure;
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
        [ModelToTempData]
        [TempDataToModel]
        public ActionResult EmailSent()
        {
            var model = ViewData.Model as EmailSentModel;

            if (model == null)
            {
                return RedirectToAction(Action.Index);
            }

            model = UpdateModelBase(model);

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
                var email = new Email();

                Mapper.Map(model, email);

                try
                {
                    _emailSenderService.Send(email);
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelErrors(e);
                }
            }

            return ModelState.IsValid ? RedirectToActionWithModel(Action.EmailSent, new EmailSentModel(model)) : RedirectToAction(Action.Index);
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