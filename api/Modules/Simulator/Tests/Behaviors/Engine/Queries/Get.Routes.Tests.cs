using System.Linq;
using NUnit.Framework;
using Shouldly;
using Trackwane.Simulator.Engine.Queries;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Tests.Behaviors.Engine.Queries
{
    internal class FindRoutesTests
    {
        [Test]
        public void When_Given_A_Correct_API_Key_Finds_Routes()
        {
            var query = new GetRoutes(new Config());
            var results = query.Execute().ToList();
            results.Count.ShouldBeGreaterThan(0);
        }
    }
}
