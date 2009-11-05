using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Models.Contact
{
    public class EmailSentModel : DefaultMasterModel
    {
        #region Properties

        public string EmailAddress { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }

        #endregion
    }
}