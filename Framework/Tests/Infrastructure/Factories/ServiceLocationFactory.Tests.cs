using System.Linq;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Tests.Fakes;

namespace Trackwane.Framework.Tests.Infrastructure.Factories
{
    internal class ServiceLocationFactory_Tests
    {
        private readonly Assembly currentAssembly = Assembly.GetExecutingAssembly();

        [Test]
        public void Registers_Listeners()
        {
            var factory = new ServiceLocationFactory(Substitute.For<IDocumentStoreBuilder>());
            var subscribers = factory.WithListeners(currentAssembly.GetListeners(), currentAssembly.GetDomainEvents()).AsSubscriberRegistry();
            subscribers.Count().ShouldBe(1);
            subscribers.Get<FrameworkChecked>().ShouldNotBeEmpty();
        }

        [Test]
        public void Registers_Handlers()
        {
            var factory = new ServiceLocationFactory(Substitute.For<IDocumentStoreBuilder>());
            var subscribers = factory.WithHandlers(currentAssembly.GetHandlers(), currentAssembly.GetCommands()).AsSubscriberRegistry();
            subscribers.Count().ShouldBe(1);
            subscribers.Get<CheckFramework>().ShouldNotBeEmpty();
        }
    }
}
