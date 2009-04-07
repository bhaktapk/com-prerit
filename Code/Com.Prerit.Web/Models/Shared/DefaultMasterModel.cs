using System;

namespace Com.Prerit.Web.Models.Shared
{
    public class DefaultMasterModel
    {
        #region Properties

        public string ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public int CopyrightBeginYear { get; set; }

        public int CopyrightEndYear { get; set; }

        public string SiteName { get; set; }

        public Uri ValidationUri { get; set; }

        #endregion
    }
}