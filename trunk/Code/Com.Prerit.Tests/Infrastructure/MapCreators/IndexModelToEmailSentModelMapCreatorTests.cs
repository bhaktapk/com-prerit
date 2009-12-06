using AutoMapper;

using Com.Prerit.Infrastructure.MapCreators;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.MapCreators
{
    [TestFixture]
    public class IndexModelToEmailSentModelMapCreatorTests
    {
        #region Tests

        [Test]
        public void Should_Create_Map()
        {
            // arrange
            Mapper.Reset();

            // act
            new IndexModelToEmailSentModelMapCreator().CreateMap();

            // assert
            Mapper.AssertConfigurationIsValid();
        }

        #endregion
    }
}