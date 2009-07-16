using AutoMapper;

using Com.Prerit.Web.Infrastructure.StartupTasks;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class AutoMapperConfigurationStartupTaskTests
    {
        #region Setup/Teardown

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            StartupTaskRunner.Run();
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Assert_Configuration_Is_Valid()
        {
            Mapper.CreateMap<Source, Destination>();

            Assert.That(() => new AutoMapperConfigurationStartupTask().Execute(), Throws.InstanceOf<AutoMapperConfigurationException>());
        }

        [Test]
        public void Should_Contain_Maps()
        {
            new AutoMapperConfigurationStartupTask().Execute();

            Assert.That(Mapper.GetAllTypeMaps(), Is.Not.Empty);
        }

        #endregion

        private class Source
        {
            public int SomeValue { get; set; }
        }

        private class Destination
        {
            public int SomeValuefff { get; set; }
        }

    }
}