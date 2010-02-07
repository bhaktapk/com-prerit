using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Com.Prerit.Domain;
using Com.Prerit.Services;

namespace Com.Prerit.Filters
{
    public class CustomAuthorize : FilterAttribute, IAuthorizationFilter
    {
        #region Fields

        private RoleType _allowedRoleTypes;

        private readonly IRoleService _roleService;

        private IEnumerable<RoleType> _splitAllowedRoleTypes = new RoleType[0];

        #endregion

        #region Properties

        public RoleType AllowedRoleTypes
        {
            get { return _allowedRoleTypes; }
            set
            {
                _allowedRoleTypes = value;

                _splitAllowedRoleTypes = from RoleType knownRole in Enum.GetValues(typeof(RoleType))
                                         where (value & knownRole) == knownRole
                                         select knownRole;
            }
        }

        #endregion

        #region Constructors

        public CustomAuthorize()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                throw new InvalidOperationException("Cannot construct object without a current HttpContext");
            }

            var cacheService = new CacheService(httpContext.Cache);

            var xmlStoreService = new XmlStoreService();

            var httpServerUtility = new HttpServerUtilityWrapper(httpContext.Server);

            _roleService = new RoleService(cacheService, xmlStoreService, httpServerUtility);
        }

        public CustomAuthorize(IRoleService roleService)
        {
            if (roleService == null)
            {
                throw new ArgumentNullException("roleService");
            }

            _roleService = roleService;
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

            IEnumerable<RoleType> userRoles = _roleService.GetRolesById(httpContext.User.Identity.Name);

            if (_splitAllowedRoleTypes.Count() != 0 && _splitAllowedRoleTypes.Intersect(userRoles).Count() == 0)
            {
                return false;
            }

            return true;
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
                filterContext.Result =
                    new RedirectResult(new UrlHelper(filterContext.RequestContext).Action(MVC.Accounts.LogIn(filterContext.HttpContext.Request.Url.PathAndQuery)));
                return;
            }

            if (!AuthorizeCore(filterContext.HttpContext))
            {
                filterContext.Result =
                    new RedirectResult(
                        new UrlHelper(filterContext.RequestContext).Action(MVC.Accounts.Unauthorized(filterContext.HttpContext.Request.Url.PathAndQuery)));
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