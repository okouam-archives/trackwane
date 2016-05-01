using Marten;
using NSubstitute;
using NUnit.Framework;
using paramore.brighter.commandprocessor;
using Shouldly;
using StructureMap;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Tests.Fakes;
using Registry = Trackwane.Framework.Infrastructure.Registry;

namespace Trackwane.Framework.Tests.Infrastructure.Factories
{
    internal class HandlerFactory_Tests
    {
        [Test]
        public void When_Creating_Uses_The_Container()
        {
            var documentStore = Substitute.For<IDocumentStore>();
            var commandProcessor = Substitute.For<IAmACommandProcessor>();
            var executionEngine = Substitute.For<IExecutionEngine>();

            var container = new Container(x =>
            {
                x.IncludeRegistry<Registry>();
                x.For<CheckFrameworkHandler>().Use<CheckFrameworkHandler>();
                x.For<FrameworkCheckedListener>().Use<FrameworkCheckedListener>();
                x.For<IDocumentStore>().Use(documentStore);
                x.For<IAmACommandProcessor>().Use(commandProcessor);
                x.For<IExecutionEngine>().Use(executionEngine);
            });

            container.AssertConfigurationIsValid();

            var handlerFactory = new HandlerFactory(container);

            handlerFactory.Create(typeof(CheckFrameworkHandler)).ShouldNotBeNull();
            handlerFactory.Create(typeof(FrameworkCheckedListener)).ShouldNotBeNull();
        }
    }
}
