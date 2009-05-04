using System;
using System.Web.Mvc;

using Castle.Components.Validator;

namespace Com.Prerit.Web
{
    public class RegisterDefaultModelBinderStartupTask : IStartupTask
    {
        #region Fields

        private readonly SimpleValidatingModelBinder _modelBinder;

        private readonly ModelBinderDictionary _modelBinderDictionary;

        #endregion

        #region Constructors

        public RegisterDefaultModelBinderStartupTask(ModelBinderDictionary modelBinderDictionary, SimpleValidatingModelBinder modelBinder)
        {
            if (modelBinderDictionary == null)
            {
                throw new ArgumentNullException("modelBinderDictionary");
            }

            if (modelBinder == null)
            {
                throw new ArgumentNullException("modelBinder");
            }

            _modelBinderDictionary = modelBinderDictionary;
            _modelBinder = modelBinder;
        }

        public RegisterDefaultModelBinderStartupTask()
            : this(ModelBinders.Binders, new SimpleValidatingModelBinder(new ValidatorRunner(new CachedValidationRegistry())))
        {
        }

        #endregion

        #region Methods

        public void Execute()
        {
            _modelBinderDictionary.DefaultBinder = _modelBinder;
        }

        #endregion
    }
}