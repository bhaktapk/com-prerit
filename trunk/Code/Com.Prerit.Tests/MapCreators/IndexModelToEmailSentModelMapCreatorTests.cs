using AutoMapper;
using AutoMapper.Mappers;

using Com.Prerit.MapCreators;

using NUnit.Framework;

namespace Com.Prerit.Tests.MapCreators
{
    [TestFixture]
    public class IndexModelToEmailSentModelMapCreatorTests
    {
        #region Tests

        [Test]
        public void Should_Create_Map()
        {
            // arrange
            var configuration = new Configuration(MapperRegistry.AllMappers());

            // act
            new IndexModelToEmailSentModelMapCreator().CreateMap(configuration);

            // assert
            Assert.That(configuration.GetAllTypeMaps(), Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Create_Valid_Map()
        {
            // arrange
            var configuration = new Configuration(MapperRegistry.AllMappers());

            // act
            new IndexModelToEmailSentModelMapCreator().CreateMap(configuration);

            // assert
            configuration.AssertConfigurationIsValid();
        }

        #endregion
    }
}