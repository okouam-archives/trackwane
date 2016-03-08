using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using paramore.brighter.commandprocessor;
using Trackwane.Data.Client;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Data.Tests.Helpers
{
    internal partial class Scenario
    {
        protected IList<IRequest> Processed { get; set; } = new List<IRequest>();

        protected IList<IRequest> Published { get; set; } = new List<IRequest>();

        protected IList<IRequest> Posted { get; set; } = new List<IRequest>();

        protected IList<IRequest> Sent { get; set; } = new List<IRequest>();

        protected IEngineHost EngineHost => Setup.EngineHost;

        protected static DataContext Client { get; set; }

        [SetUp]
        public void BeforeEachTest()
        {
            Client = new DataContext(Setup.EngineHost.Configuration.ListenUri.ToString());

            EngineHost.ExecutionEngine.MessageProcessed += (o, request) => Processed.Add(request); ;

            EngineHost.ExecutionEngine.MessagePublished += (o, request) => Published.Add(request); ;

            EngineHost.ExecutionEngine.MessageSent += (o, request) => Sent.Add(request); ;

            EngineHost.ExecutionEngine.MessagePosted += (o, request) => Posted.Add(request); ;
        }

        protected bool WasPosted<T>()
        {
            return Posted.Any(x => x.GetType() == typeof(T));
        }
    }
}
