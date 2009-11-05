using AutoMapper;

using Com.Prerit.Web.Infrastructure.MapCreators;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.MapCreators
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