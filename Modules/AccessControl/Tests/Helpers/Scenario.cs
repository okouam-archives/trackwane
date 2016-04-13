using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using paramore.brighter.commandprocessor;
using Trackwane.AccessControl.Contracts;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        public Scenario()
        {
            Processed = new List<IRequest>();
            Sent = new List<IRequest>();
            Published = new List<IRequest>();
            Posted = new List<IRequest>();
        }

        protected IList<IRequest> Processed { get; set; }

        protected IList<IRequest> Published { get; set; }

        protected IList<IRequest> Posted { get; set; }

        protected IList<IRequest> Sent { get; set; }

        protected IEngineHost EngineHost
        {
            get { return Setup.EngineHost; }
        }

        protected static AccessControlContext Client { get; set; }
        
        [SetUp]
        public void BeforeEachTest()
        {
            Client = new AccessControlContext(Setup.EngineHost.Configuration.ListenUri.ToString(), new PlatformConfig());

            EngineHost.ExecutionEngine.MessageProcessed += (o, request) => Processed.Add(request);

            EngineHost.ExecutionEngine.MessagePublished += (o, request) => Published.Add(request);

            EngineHost.ExecutionEngine.MessageSent += (o, request) => Sent.Add(request);

            EngineHost.ExecutionEngine.MessagePosted += (o, request) => Posted.Add(request);
        }

        protected bool WasPosted<T>()
        {
            return Posted.Any(x => x.GetType() == typeof (T));
        }
    }
}
