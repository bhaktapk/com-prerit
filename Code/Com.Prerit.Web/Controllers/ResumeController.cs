﻿using System.Web.Mvc;

using Com.Prerit.Web.Models.Resume;

namespace Com.Prerit.Web.Controllers
{
    public class ResumeController : DefaultMasterController
    {
        #region Methods

        public ActionResult Index()
        {
            var model = CreateBaseModel<IndexModel>();

            return View(model);
        }

        #endregion
    }
}