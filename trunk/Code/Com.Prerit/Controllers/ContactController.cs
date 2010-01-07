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
    public partial class ContactController : Controller
    {
        #region Fields

        private readonly IEmailSenderService _emailSenderService;

        public static readonly string SeoName = ControllerUtil.GetSeoControllerName<ContactController>();

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
        [ModelToTempData]
        [TempDataToModel]
        public virtual ActionResult EmailSent()
        {
            var model = ViewData.Model as EmailSentModel;

            if (model == null)
            {
                return RedirectToAction(MVC.Contact.Index());
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ModelStateToTempData]
        public virtual ActionResult Index()
        {
            var model = new IndexModel();

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ModelToTempData]
        [ModelStateToTempData]
        public virtual ActionResult SendEmail(IndexModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(MVC.Contact.Index());
            }

            Email email = Mapper.Map<IndexModel, Email>(model);

            try
            {
                _emailSenderService.Send(email);
            }
            catch (ValidationException e)
            {
                ModelState.AddModelErrors(e);

                return RedirectToAction(MVC.Contact.Index());
            }

            ViewData.Model = Mapper.Map<IndexModel, EmailSentModel>(model);

            return RedirectToAction(MVC.Contact.EmailSent());
        }

        #endregion
    }
}