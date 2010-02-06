using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Com.Prerit.Services;

namespace Com.Prerit.Filters
{
    public class AuthorizeAdminAttribute : FilterAttribute, IAuthorizationFilter
    {
        #region Fields

        private readonly IRolesService _rolesService;

        #endregion

        #region Constructors

        public AuthorizeAdminAttribute()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                throw new InvalidOperationException("Cannot construct object without a current HttpContext");
            }

            var cacheService = new CacheService(httpContext.Cache);

            var xmlStoreService = new XmlStoreService();

            var httpServerUtility = new HttpServerUtilityWrapper(httpContext.Server);

            _rolesService = new RolesService(cacheService, xmlStoreService, httpServerUtility);
        }

        public AuthorizeAdminAttribute(IRolesService rolesService)
        {
            if (rolesService == null)
            {
                throw new ArgumentNullException("rolesService");
            }

            _rolesService = rolesService;
        }

        #endregion

        #region Methods

        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return _rolesService.GetAdminAccounts().Any(a => a.Id == httpContext.User.Identity.Name);
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(new UrlHelper(filterContext.RequestContext).Action(MVC.Accounts.LogIn(filterContext.HttpContext.Request.Url.PathAndQuery)));
                return;
            }

            if (!AuthorizeCore(filterContext.HttpContext))
            {
                filterContext.Result = new RedirectResult(new UrlHelper(filterContext.RequestContext).Action(MVC.Accounts.Unauthorized(filterContext.HttpContext.Request.Url.PathAndQuery)));
                return;
            }

            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;

            cache.SetProxyMaxAge(new TimeSpan(0));
            cache.AddValidationCallback(CacheValidateHandler, null);
        }

        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextWrapper httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!AuthorizeCore(httpContext))
            {
                return HttpValidationStatus.IgnoreThisRequest;
            }

            return HttpValidationStatus.Valid;
        }

        #endregion
    }
}