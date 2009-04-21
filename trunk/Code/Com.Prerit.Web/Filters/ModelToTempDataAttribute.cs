using System.Web.Mvc;

namespace Com.Prerit.Web.Filters
{
    public class ModelToTempDataAttribute : ActionFilterAttribute
    {
        #region Constants

        public const string TempDataKey = "temp-data-model";

        #endregion

        #region Methods

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ActionResult result = filterContext.Result;

            ControllerBase controller = filterContext.Controller;

            object model = filterContext.Controller.ViewData.Model;

            if (model != null && result is RedirectToRouteResult || result is RedirectResult)
            {
                controller.TempData[TempDataKey] = model;
            }
        }

        #endregion
    }
}