using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Com.Prerit.Core;
using Com.Prerit.Domain;
using Com.Prerit.Security;

namespace Com.Prerit.Filters
{
    public class CustomAuthorize : FilterAttribute, IAuthorizationFilter
    {
        #region Fields

        private RoleType _allowedRoleTypes;

        private IEnumerable<RoleType> _splitAllowedRoleTypes = new RoleType[0];

        #endregion

        #region Properties

        public RoleType AllowedRoleTypes
        {
            get { return _allowedRoleTypes; }
            set
            {
                _allowedRoleTypes = value;

                _splitAllowedRoleTypes = (from RoleType knownRole in Enum.GetValues(typeof(RoleType))
                                          where (value & knownRole) == knownRole
                                          select knownRole).ExecuteQuery();
            }
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

            if (_splitAllowedRoleTypes.Count() > 0 && !_splitAllowedRoleTypes.Any(roleType => httpContext.User.IsInRole(roleType)))
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
                var urlHelper = new UrlHelper(filterContext.RequestContext);

                filterContext.Result = new RedirectResult(urlHelper.Action(MVC.Accounts.LogIn(filterContext.HttpContext.Request.Url.PathAndQuery)));
                return;
            }

            if (!AuthorizeCore(filterContext.HttpContext))
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);

                filterContext.Result = new RedirectResult(urlHelper.Action(MVC.Accounts.Unauthorized(filterContext.HttpContext.Request.Url.PathAndQuery)));
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