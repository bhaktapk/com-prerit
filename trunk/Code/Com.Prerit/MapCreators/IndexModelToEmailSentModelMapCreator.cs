using AutoMapper;

using Com.Prerit.Models.Contact;

namespace Com.Prerit.MapCreators
{
    public class IndexModelToEmailSentModelMapCreator : IMapCreator
    {
        #region Methods

        public void CreateMap()
        {
            Mapper.CreateMap<IndexModel, EmailSentModel>();
        }

        #endregion
    }
}