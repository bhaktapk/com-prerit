using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Castle.Components.Validator;

using xVal.ServerSide;

namespace Com.Prerit.Web
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

            foreach (ErrorInfo error in GetErrors(model))
            {
                bindingContext.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return model;
        }

        private IList<ErrorInfo> GetErrors(object instance)
        {
            var result = new List<ErrorInfo>();

            if (instance != null && !_runner.IsValid(instance))
            {
                ErrorSummary errorSummary = _runner.GetErrorSummary(instance);

                IEnumerable<ErrorInfo> errorInfos = from prop in errorSummary.InvalidProperties
                                                    from err in errorSummary.GetErrorsForProperty(prop)
                                                    select new ErrorInfo(prop, err);

                result.AddRange(errorInfos);
            }

            return result;
        }

        #endregion
    }
}