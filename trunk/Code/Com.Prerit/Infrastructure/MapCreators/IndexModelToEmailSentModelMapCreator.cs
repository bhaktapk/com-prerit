using AutoMapper;

using Com.Prerit.Web.Models.Contact;

namespace Com.Prerit.Web.Infrastructure.MapCreators
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