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
                .ForMember(s => s.FromEmailAddress, opt => opt.MapFrom(d => EmailInfo.AuthorEmailAddress))
                .ForMember(s => s.ToEmailAddress, opt => opt.MapFrom(d => d.EmailAddress))
                .ForMember(s => s.Subject, opt => opt.MapFrom(d => EmailInfo.GetContactEmailSubject(d.Name)));
        }

        #endregion
    }
}