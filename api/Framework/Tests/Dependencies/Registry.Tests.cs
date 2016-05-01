using Marten;
using NSubstitute;
using NUnit.Framework;
using paramore.brighter.commandprocessor;
using StructureMap;
using Registry = Trackwane.Framework.Infrastructure.Registry;

namespace Trackwane.Framework.Tests.Dependencies
{
    internal class Registry_Tests
    {
        [Test]
        public void Has_Valid_Configuration()
        {
            var container = new Container(x =>
            {
                var fakeDocumentStore = Substitute.For<IDocumentStore>();
                x.For<IDocumentStore>().Use(fakeDocumentStore);

                var fakeCommandProcessor = Substitute.For<IAmACommandProcessor>();
                x.For<IAmACommandProcessor>().Use(fakeCommandProcessor);

                x.IncludeRegistry<Registry>();
            });

            container.AssertConfigurationIsValid();
        }
    }
}
