using System;
using System.Web;
using System.Web.Mvc;

using Castle.Windsor;

namespace Com.Prerit.Web
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        #region Fields

        private readonly IWindsorContainer _container;

        #endregion

        #region Constructors

        public WindsorControllerFactory(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        #endregion

        #region Methods

        protected override IController GetControllerInstance(Type controllerType)
        {
            if (controllerType == null)
            {
                const string message = "The controller for path '{0}' could not be found or it does not implement IController.";

                throw new HttpException(404, string.Format(message, RequestContext.HttpContext.Request.Path));
            }

            return (IController) _container.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }

            _container.Release(controller);
        }

        #endregion
    }
}