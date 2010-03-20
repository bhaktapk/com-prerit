using System;
using System.Web.Mvc;

using AutoMapper;

using Castle.Components.Validator;

using Com.Prerit.Domain;
using Com.Prerit.Filters;
using Com.Prerit.Models.Contact;
using Com.Prerit.Services;

using MvcContrib.Filters;

namespace Com.Prerit.Controllers
{
    public partial class ContactController : Controller
    {
        #region Fields

        private readonly IEmailSenderService _emailSenderService;

        private readonly IMappingEngine _mapper;

        #endregion

        #region Constructors

        public ContactController(IEmailSenderService emailSenderService, IMappingEngine mapper)
        {
            if (emailSenderService == null)
            {
                throw new ArgumentNullException("emailSenderService");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            _emailSenderService = emailSenderService;
            _mapper = mapper;
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

            Email email = _mapper.Map<IndexModel, Email>(model);

            try
            {
                _emailSenderService.Send(email);
            }
            catch (ValidationException e)
            {
                ControllerUtil.AddModelErrors(ModelState, e);

                return RedirectToAction(MVC.Contact.Index());
            }

            ViewData.Model = _mapper.Map<IndexModel, EmailSentModel>(model);

            return RedirectToAction(MVC.Contact.EmailSent());
        }

        #endregion
    }
}