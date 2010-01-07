using AutoMapper;

using Com.Prerit.MapCreators;

using NUnit.Framework;

namespace Com.Prerit.Tests.MapCreators
{
    [TestFixture]
    public class IndexModelToEmailMapCreatorTests
    {
        #region Tests

        [Test]
        public void Should_Create_Map()
        {
            // arrange
            Mapper.Reset();

            // act
            new IndexModelToEmailMapCreator().CreateMap();

            // assert
            Mapper.AssertConfigurationIsValid();
        }

        #endregion
    }
}