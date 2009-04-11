using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Models.Contact
{
    public class EmailSentModel : DefaultMasterModel
    {
        #region Properties

        public string EmailAddress { get; private set; }

        public string Message { get; private set; }

        public string Name { get; private set; }

        #endregion

        #region Constructors

        public EmailSentModel(IndexModel indexModel)
        {
            EmailAddress = indexModel.EmailAddress;
            Message = indexModel.Message;
            Name = indexModel.Name;
        }

        #endregion
    }
}