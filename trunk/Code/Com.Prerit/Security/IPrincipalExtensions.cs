using System;
using System.Security.Principal;

using Com.Prerit.Domain;

namespace Com.Prerit.Security
{
    public static class IPrincipalExtensions
    {
        #region Methods

        public static bool IsInRole(this IPrincipal principal, RoleType roleType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }

            return principal.IsInRole(Enum.GetName(typeof(RoleType), roleType));
        }

        #endregion
    }
}