using NUnit.Framework;
using paramore.brighter.commandprocessor;
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
            var container = new Container();

            Should.NotThrow(() =>
            {
                CommandProcessorFactory.Build(null, null,
                    new SubscriberRegistry(), container, 
                    new MapperFactory(new Container()));
            });
        }
    }
}
