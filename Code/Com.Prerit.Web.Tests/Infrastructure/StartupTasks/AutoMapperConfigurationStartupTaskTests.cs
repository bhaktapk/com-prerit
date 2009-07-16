using AutoMapper;

using Com.Prerit.Web.Infrastructure.StartupTasks;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class AutoMapperConfigurationStartupTaskTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            StartupTaskRunner.Run();
        }

        [TearDown]
        public void TearDown()
        {
            StartupTaskRunner.Reset();
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

        [Test]
        public void Should_Reset_Mapper()
        {
            var task = new AutoMapperConfigurationStartupTask();

            task.Execute();
            task.Reset();

            Assert.That(Mapper.GetAllTypeMaps(), Is.Empty);
        }

        #endregion

        #region Nested Type: Destination

        private class Destination
        {
            #region Properties

            public int SomeValuefff { get; set; }

            #endregion
        }

        #endregion

        #region Nested Type: Source

        private class Source
        {
            #region Properties

            public int SomeValue { get; set; }

            #endregion
        }

        #endregion
    }
}