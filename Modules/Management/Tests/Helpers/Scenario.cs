using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using paramore.brighter.commandprocessor;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Client;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected IList<IRequest> Processed { get; set; } = new List<IRequest>();

        protected IList<IRequest> Published { get; set; } = new List<IRequest>();

        protected IList<IRequest> Posted { get; set; } = new List<IRequest>();

        protected IList<IRequest> Sent { get; set; } = new List<IRequest>();

        protected static IEngineHost EngineHost => Setup.EngineHost;

        protected static ManagementContext Client { get; set; }

        [SetUp]
        public void BeforeEachTest()
        {
            Client = new ManagementContext(Setup.EngineHost.Configuration.ListenUri.ToString());

            EngineHost.ExecutionEngine.MessageProcessed += (o, request) => Processed.Add(request); 

            EngineHost.ExecutionEngine.MessagePublished += (o, request) => Published.Add(request); 

            EngineHost.ExecutionEngine.MessageSent += (o, request) => Sent.Add(request); 

            EngineHost.ExecutionEngine.MessagePosted += (o, request) => Posted.Add(request); 
        }

        protected bool WasPosted<T>()
        {
            return Posted.Any(x => x.GetType() == typeof(T));
        }
    }
}
