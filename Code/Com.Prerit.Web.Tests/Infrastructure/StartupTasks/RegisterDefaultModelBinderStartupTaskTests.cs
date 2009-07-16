using System.Web.Mvc;

using Com.Prerit.Web.Infrastructure.ModelBinders;
using Com.Prerit.Web.Infrastructure.StartupTasks;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class RegisterDefaultModelBinderStartupTaskTests
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
        public void Should_Reset_Binder()
        {
            var task = new RegisterDefaultModelBinderStartupTask();

            task.Execute();
            task.Reset();

            Assert.That(ModelBinders.Binders.DefaultBinder, Is.InstanceOf<DefaultModelBinder>());
        }

        [Test]
        public void Should_Set_Default_Binder()
        {
            new RegisterDefaultModelBinderStartupTask().Execute();

            Assert.That(ModelBinders.Binders.DefaultBinder, Is.InstanceOf<SimpleValidatingModelBinder>());
        }

        #endregion
    }
}