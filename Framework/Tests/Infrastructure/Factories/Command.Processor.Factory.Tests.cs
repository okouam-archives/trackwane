using NUnit.Framework;
using paramore.brighter.commandprocessor;
using Raven.Client;
using Raven.Client.Document;
using Shouldly;
using StructureMap;
using Trackwane.Framework.Infrastructure.Factories;

namespace Trackwane.Framework.Tests.Infrastructure.Factories
{
    internal class CommandProcessorFactory_Tests
    {
        [Test]
        public void Builds_Command_Processors()
        {
            var container = new Container(x =>
            {
                x.For<IDocumentStore>().Use(new DocumentStore());
            });

            Should.NotThrow(() =>
            {
                CommandProcessorFactory.Build(
                    new SubscriberRegistry(), container, 
                    new MapperFactory(new Container()));
            });
        }
    }
}
