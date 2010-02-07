using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IRolesService
    {
        #region Methods

        IEnumerable<string> GetIdsByRole(KnownRole role);

        IEnumerable<KnownRole> GetRolesById(string id);

        #endregion
    }
}