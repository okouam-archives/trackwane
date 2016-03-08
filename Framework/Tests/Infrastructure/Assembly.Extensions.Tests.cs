using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Infrastructure;

namespace Trackwane.Framework.Tests.Infrastructure
{
    class AssemblyExtensions_Tests
    {
        [Test]
        public void When_Getting_Domain_Events_Only_Returns_Domain_Events()
        {
            var events = Assembly.GetExecutingAssembly().GetDomainEvents().ToList();
            events.Count.ShouldBe(1);
        }

        [Test]
        public void When_Getting_Commands_Only_Returns_Commands()
        {
            var commands = Assembly.GetExecutingAssembly().GetCommands().ToList();
            commands.Count.ShouldBe(1);
        }

        [Test]
        public void When_Getting_Handlers_Only_Returns_Handlers()
        {
            var handlers = Assembly.GetExecutingAssembly().GetHandlers().ToList();
            handlers.Count.ShouldBe(1);
        }

        [Test]
        public void When_Getting_Listeners_Only_Returns_Handlers()
        {
            var listeners = Assembly.GetExecutingAssembly().GetListeners().ToList();
            listeners.Count.ShouldBe(1);
        }
    }
}
