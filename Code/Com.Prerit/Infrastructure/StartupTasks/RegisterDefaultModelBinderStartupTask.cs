using System.Web.Mvc;

using Com.Prerit.Infrastructure.ModelBinders;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class RegisterDefaultModelBinderStartupTask : IStartupTask
    {
        #region Methods

        public void Execute()
        {
            System.Web.Mvc.ModelBinders.Binders.DefaultBinder = ServiceLocator.Current.GetInstance<SimpleValidatingModelBinder>();
        }

        public void Reset()
        {
            System.Web.Mvc.ModelBinders.Binders.DefaultBinder = new DefaultModelBinder();
        }

        #endregion
    }
}