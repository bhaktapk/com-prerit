using AutoMapper;

using Com.Prerit.Web.Infrastructure.MapCreators;
using Com.Prerit.Web.Infrastructure.StartupTasks;

using Microsoft.Practices.ServiceLocation;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class AutoMapperConfigurationStartupTaskTests
    {
        #region Tests

        [Test]
        public void Should_Assert_Configuration_Is_Valid()
        {
            // arrange
            var serviceLocator = new Mock<IServiceLocator>();
            var mapCreator = new Mock<IMapCreator>();

            mapCreator.Setup(mc => mc.CreateMap()).Callback(() => Mapper.CreateMap<Source, Destination>());

            serviceLocator.Setup(sl => sl.GetAllInstances<IMapCreator>()).Returns(new[]
                                                                                      {
                                                                                          mapCreator.Object
                                                                                      });

            ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);

            // act
            TestDelegate act = () => new AutoMapperConfigurationStartupTask().Execute();

            // assert
            Assert.That(act, Throws.InstanceOf<AutoMapperConfigurationException>());

            // teardown
            Mapper.Reset();
            ServiceLocator.SetLocatorProvider(() => null);
        }

        [Test]
        public void Should_Reset_Mapper()
        {
            // arrange
            Mapper.CreateMap<Source, Destination>();

            // act
            new AutoMapperConfigurationStartupTask().Reset();

            // assert
            Assert.That(Mapper.GetAllTypeMaps(), Is.Empty);
        }

        #endregion

        #region Nested Type: Destination

        private class Destination
        {
            #region Properties

            public int PropertyB { private get; set; }

            #endregion

            #region Constructors

            public Destination(int propertyB)
            {
                PropertyB = propertyB;
            }

            #endregion
        }

        #endregion

        #region Nested Type: Source

        private class Source
        {
            #region Properties

            public int PropertyA { private get; set; }

            #endregion

            #region Constructors

            public Source(int propertyA)
            {
                PropertyA = propertyA;
            }

            #endregion
        }

        #endregion
    }
}