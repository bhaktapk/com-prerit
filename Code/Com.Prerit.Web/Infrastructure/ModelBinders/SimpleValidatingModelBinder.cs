using System;
using System.Web.Mvc;

using Castle.Components.Validator;

namespace Com.Prerit.Web.Infrastructure.ModelBinders
{
    public class SimpleValidatingModelBinder : DefaultModelBinder
    {
        #region Fields

        private readonly IValidatorRunner _runner;

        #endregion

        #region Constructors

        public SimpleValidatingModelBinder(IValidatorRunner runner)
        {
            if (runner == null)
            {
                throw new ArgumentNullException("runner");
            }

            _runner = runner;
        }

        #endregion

        #region Methods

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = base.BindModel(controllerContext, bindingContext);

            if (model != null && !_runner.IsValid(model))
            {
                bindingContext.ModelState.AddModelErrors(_runner.GetErrorSummary(model));
            }

            return model;
        }

        #endregion
    }
}