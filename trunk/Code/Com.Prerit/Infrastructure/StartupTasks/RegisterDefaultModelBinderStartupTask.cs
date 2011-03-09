using System;
using System.Web.Mvc;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class RegisterDefaultModelBinderStartupTask : IStartupTask
    {
        #region Fields

        private readonly IModelBinder _modelBinder;

        #endregion

        #region Constructors

        public RegisterDefaultModelBinderStartupTask(IModelBinder modelBinder)
        {
            if (modelBinder == null)
            {
                throw new ArgumentNullException("modelBinder");
            }

            _modelBinder = modelBinder;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            System.Web.Mvc.ModelBinders.Binders.DefaultBinder = _modelBinder;
        }

        public void Reset()
        {
            System.Web.Mvc.ModelBinders.Binders.DefaultBinder = new DefaultModelBinder();
        }

        #endregion
    }
}