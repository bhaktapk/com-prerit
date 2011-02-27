using AutoMapper;

using Com.Prerit.Infrastructure.StartupTasks;
using Com.Prerit.MapCreators;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class AutoMapperConfigurationStartupTaskTests
    {
        #region Tests

        [Test]
        public void Should_Assert_Configuration_Is_Valid()
        {
            // arrange
            var configurationProvider = new Mock<IConfigurationProvider>();
            var profileExpression = new Mock<IProfileExpression>();
            var mapCreators = new IMapCreator[] { };

            // act
            new AutoMapperConfigurationStartupTask(configurationProvider.Object, profileExpression.Object, mapCreators).Execute();

            // assert
            configurationProvider.Verify(configProvider => configProvider.AssertConfigurationIsValid());
        }

        [Test]
        public void Should_Create_Maps_Via_Map_Creators()
        {
            // arrange
            var configurationProvider = new Mock<IConfigurationProvider>();
            var profileExpression = new Mock<IProfileExpression>();
            var mapCreator = new Mock<IMapCreator>();

            // act
            new AutoMapperConfigurationStartupTask(configurationProvider.Object, profileExpression.Object, new[] { mapCreator.Object, mapCreator.Object }).Execute();

            // assert
            mapCreator.Verify(creator => creator.CreateMap(profileExpression.Object), Times.Exactly(2));
        }

        [Test]
        public void Should_Reset_Mapper()
        {
            //NOTE: there isn't a need to reset the maps because IConfigurationProvider and IProfileExpression should be new instances on subsequent calls to Execute
        }

        #endregion
    }
}