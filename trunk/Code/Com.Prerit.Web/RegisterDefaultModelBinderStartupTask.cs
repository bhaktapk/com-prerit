using System;
using System.Web.Mvc;

using Microsoft.Web.Mvc.DataAnnotations;

namespace Com.Prerit.Web
{
    public class RegisterDefaultModelBinderStartupTask : IStartupTask
    {
        #region Fields

        private readonly ModelBinderDictionary _modelBinderDictionary;

        #endregion

        #region Constructors

        public RegisterDefaultModelBinderStartupTask(ModelBinderDictionary modelBinderDictionary)
        {
            if (modelBinderDictionary == null)
            {
                throw new ArgumentNullException("modelBinderDictionary");
            }

            _modelBinderDictionary = modelBinderDictionary;
        }

        public RegisterDefaultModelBinderStartupTask()
            : this(ModelBinders.Binders)
        {
        }

        #endregion

        #region Methods

        public void Execute()
        {
            _modelBinderDictionary.DefaultBinder = new DataAnnotationsModelBinder();
        }

        #endregion
    }
}