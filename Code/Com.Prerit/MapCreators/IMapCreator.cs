using AutoMapper;

namespace Com.Prerit.MapCreators
{
    public interface IMapCreator
    {
        #region Methods

        void CreateMap(IProfileExpression profileExpression);

        #endregion
    }
}