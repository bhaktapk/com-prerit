using System.Collections.Generic;

namespace Com.Prerit.Services
{
    public interface IRolesService
    {
        #region Methods

        IEnumerable<string> GetIdsByRole(string roleName);

        #endregion
    }
}