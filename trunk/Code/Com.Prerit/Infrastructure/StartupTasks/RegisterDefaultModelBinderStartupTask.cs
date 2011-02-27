using System;
using System.Web.Mvc;

using Com.Prerit.Infrastructure.ModelBinders;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class RegisterDefaultModelBinderStartupTask : IStartupTask
    {
        #region Fields

        private readonly SimpleValidatingModelBinder _simpleValidatingModelBinder;

        #endregion

        #region Constructors

        public RegisterDefaultModelBinderStartupTask(SimpleValidatingModelBinder simpleValidatingModelBinder)
        {
            if (simpleValidatingModelBinder == null)
            {
                throw new ArgumentNullException("simpleValidatingModelBinder");
            }

            _simpleValidatingModelBinder = simpleValidatingModelBinder;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            System.Web.Mvc.ModelBinders.Binders.DefaultBinder = _simpleValidatingModelBinder;
        }

        public void Reset()
        {
            System.Web.Mvc.ModelBinders.Binders.DefaultBinder = new DefaultModelBinder();
        }

        #endregion
    }
}