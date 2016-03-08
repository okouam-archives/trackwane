using System.Linq;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Tests.Fakes;

namespace Trackwane.Framework.Tests.Infrastructure
{
    internal class ServiceLocationFactory_Tests
    {
        [Test]
        public void Registers_Listeners()
        {
            var factory = new ServiceLocationFactory(Substitute.For<IDocumentStoreBuilder>());
            var subscribers = factory.WithListeners(Assembly.GetExecutingAssembly(), Assembly.GetExecutingAssembly()).AsSubscriberRegistry();
            subscribers.Count().ShouldBe(1);
            subscribers.Get<FrameworkChecked>().ShouldNotBeEmpty();
        }

        [Test]
        public void Registers_Handlers()
        {
            var factory = new ServiceLocationFactory(Substitute.For<IDocumentStoreBuilder>());
            var subscribers = factory.WithHandlers(Assembly.GetExecutingAssembly(), Assembly.GetExecutingAssembly()).AsSubscriberRegistry();
            subscribers.Count().ShouldBe(1);
            subscribers.Get<CheckFramework>().ShouldNotBeEmpty();
        }
    }
}
