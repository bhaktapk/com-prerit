using System;
using System.Web.Mvc;

using AutoMapper;

using Castle.Components.Validator;

using Com.Prerit.Domain;
using Com.Prerit.Filters;
using Com.Prerit.Infrastructure;
using Com.Prerit.Models.Contact;
using Com.Prerit.Services;

using MvcContrib.Filters;

namespace Com.Prerit.Controllers
{
    public class ContactController : Controller
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

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName(Action.Index)]
        [ModelStateToTempData]
        public ActionResult Index()
        {
            var model = new IndexModel();

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName(Action.SendEmail)]
        [ModelToTempData]
        [ModelStateToTempData]
        public ActionResult SendEmail(IndexModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(Action.Index);
            }

            var email = Mapper.Map<IndexModel, Email>(model);

            try
            {
                _emailSenderService.Send(email);
            }
            catch (ValidationException e)
            {
                ModelState.AddModelErrors(e);

                return RedirectToAction(Action.Index);
            }

            ViewData.Model = Mapper.Map<IndexModel, EmailSentModel>(model);

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

            public static readonly string Seo = ControllerUtil.GetSeoControllerName<ContactController>();

            public static readonly string WithoutSuffix = ControllerUtil.GetControllerNameWithoutSuffix<ContactController>();

            #endregion
        }

        #endregion
    }
}