using System.Web.Mvc;

using Castle.Components.Validator;

using Com.Prerit.Infrastructure.ModelBinders;
using Com.Prerit.Infrastructure.StartupTasks;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class RegisterDefaultModelBinderStartupTaskTests
    {
        #region Tests

        [Test]
        public void Should_Reset_Binder()
        {
            // arrange
            var runner = new Mock<IValidatorRunner>();
            var simpleValidatingModelBinder = new SimpleValidatingModelBinder(runner.Object);

            ModelBinders.Binders.DefaultBinder = null;

            // act
            new RegisterDefaultModelBinderStartupTask(simpleValidatingModelBinder).Reset();

            // assert
            Assert.That(ModelBinders.Binders.DefaultBinder, Is.InstanceOf<DefaultModelBinder>());
        }

        [Test]
        public void Should_Set_Default_Binder()
        {
            // arrange
            var runner = new Mock<IValidatorRunner>();
            var simpleValidatingModelBinder = new SimpleValidatingModelBinder(runner.Object);

            ModelBinders.Binders.DefaultBinder = new DefaultModelBinder();

            // act
            new RegisterDefaultModelBinderStartupTask(simpleValidatingModelBinder).Execute();

            // assert
            Assert.That(ModelBinders.Binders.DefaultBinder, Is.InstanceOf<SimpleValidatingModelBinder>());
        }

        #endregion
    }
}