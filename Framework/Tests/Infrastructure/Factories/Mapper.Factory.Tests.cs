using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Shouldly;
using StructureMap;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Tests.Fakes;

namespace Trackwane.Framework.Tests.Infrastructure.Factories
{
    public class MapperFactory_Tests
    {
        [Test]
        public void Creates_Mappers_For_All_Events()
        {
            var factory = new MapperFactory(new Container());

            var mappers = factory.WithEvents(Assembly.GetExecutingAssembly()).CreateMappers();
            mappers.Count().ShouldBe(1);

            var mapper = mappers.First();
            mapper.Key.Name.ShouldBe("FrameworkChecked");
            mapper.Value.Name.ShouldBe("RequestMapper`1");
        }

        [Test]
        public void When_Creating_Retrieves_Message_Mappers()
        {
            var factory = new MapperFactory(new Container());

            var mappers = factory.WithEvents(Assembly.GetExecutingAssembly()).CreateMappers();
            var mapper = factory.Create(mappers.First().Value);
            mapper.ShouldBeOfType<RequestMapper<FrameworkChecked>>();
        }

        [Test]
        public void Creates_Mappers_For_All_Commands()
        {
            var factory = new MapperFactory(new Container());
            
            var mappers = factory.WithCommands(Assembly.GetExecutingAssembly()).CreateMappers();
            mappers.Count().ShouldBe(1);

            var mapper = mappers.First();
            mapper.Key.Name.ShouldBe("CheckFramework");
            mapper.Value.Name.ShouldBe("RequestMapper`1");
        }
    }
}
