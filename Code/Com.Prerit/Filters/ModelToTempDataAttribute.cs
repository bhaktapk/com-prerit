using System.Web.Mvc;

namespace Com.Prerit.Filters
{
    public class ModelToTempDataAttribute : ActionFilterAttribute
    {
        #region Constants

        public const string TempDataKey = "temp-data-model";

        #endregion

        #region Methods

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ControllerBase controller = filterContext.Controller;

            object model = filterContext.Controller.ViewData.Model;

            if (model != null)
            {
                controller.TempData[TempDataKey] = model;
            }
        }

        #endregion
    }
}