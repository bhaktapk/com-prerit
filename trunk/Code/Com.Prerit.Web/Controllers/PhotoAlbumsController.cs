﻿using System.Web.Mvc;

using Com.Prerit.Web.Models.PhotoAlbums;

namespace Com.Prerit.Web.Controllers
{
    public class PhotoAlbumsController : DefaultMasterController
    {
        #region Methods

        [ActionName(Action.Index)]
        public ActionResult Index()
        {
            var model = UpdateModelBase(new IndexModel());

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

            public static readonly string Seo = GetSeoControllerName<PhotoAlbumsController>();

            public static readonly string WithoutSuffix = GetControllerNameWithoutSuffix<PhotoAlbumsController>();

            #endregion
        }

        #endregion
    }
}