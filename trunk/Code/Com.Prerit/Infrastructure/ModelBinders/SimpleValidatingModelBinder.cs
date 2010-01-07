using System;
using System.Linq;
using System.Web.Mvc;

using Castle.Components.Validator;

namespace Com.Prerit.Infrastructure.ModelBinders
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

        private static void AddModelErrors(ModelStateDictionary modelState, ErrorSummary errorSummary)
        {
            if (errorSummary == null)
            {
                throw new ArgumentNullException("errorSummary");
            }

            var errorInfos = from property in errorSummary.InvalidProperties
                             from message in errorSummary.GetErrorsForProperty(property)
                             select new
                                        {
                                            PropertyName = property,
                                            ErrorMessage = message
                                        };

            foreach (var errorInfo in errorInfos)
            {
                modelState.AddModelError(errorInfo.PropertyName, errorInfo.ErrorMessage);
            }
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = base.BindModel(controllerContext, bindingContext);

            if (model != null && !_runner.IsValid(model))
            {
                AddModelErrors(bindingContext.ModelState, _runner.GetErrorSummary(model));
            }

            return model;
        }

        #endregion
    }
}