using AutoMapper;

using Com.Prerit.Domain;
using Com.Prerit.Models.Contact;

namespace Com.Prerit.MapCreators
{
    public class IndexModelToEmailMapCreator : IMapCreator
    {
        #region Methods

        public void CreateMap()
        {
            Mapper.CreateMap<IndexModel, Email>()
                .ForMember(d => d.FromEmailAddress, opt => opt.MapFrom(s => s.EmailAddress))
                .ForMember(d => d.ToEmailAddress, opt => opt.MapFrom(s => "prerit.bhakta@gmail.com"))
                .ForMember(d => d.Subject, opt => opt.MapFrom(s => string.Format("prerit.com user, '{0}', is contacting you", s.Name)));
        }

        #endregion
    }
}