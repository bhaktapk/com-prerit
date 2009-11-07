using AutoMapper;

using Com.Prerit.Domain;
using Com.Prerit.Web.Models.Contact;

namespace Com.Prerit.Web.Infrastructure.MapCreators
{
    public class IndexModelToEmailMapCreator : IMapCreator
    {
        #region Methods

        public void CreateMap()
        {
            Mapper.CreateMap<IndexModel, Email>()
                .ForMember(d => d.FromEmailAddress, opt => opt.MapFrom(s => s.EmailAddress))
                .ForMember(d => d.ToEmailAddress, opt => opt.MapFrom(s => EmailInfo.AuthorEmailAddress))
                .ForMember(d => d.Subject, opt => opt.MapFrom(s => EmailInfo.GetContactEmailSubject(s.Name)));
        }

        #endregion
    }
}