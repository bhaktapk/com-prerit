using System.Web.Mvc;

namespace Com.Prerit.Web.Filters
{
    public class TempDataToModelAttribute : ActionFilterAttribute
    {
        #region Methods

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object model = filterContext.Controller.TempData[ModelToTempDataAttribute.TempDataKey];

            ViewDataDictionary viewData = filterContext.Controller.ViewData;

            if (model != null)
            {
                viewData.Model = model;
            }
        }

        #endregion
    }
}