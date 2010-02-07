using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IRoleService
    {
        #region Methods

        IEnumerable<string> GetIdsByRole(RoleType roleType);

        IEnumerable<RoleType> GetRolesById(string id);

        #endregion
    }
}