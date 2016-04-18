using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using paramore.brighter.commandprocessor;
using Trackwane.Data.Shared.Client;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Data.Tests.Helpers
{
    internal partial class Scenario
    {
        public Scenario()
        {
            Posted = new List<IRequest>();
        }

        protected IList<IRequest> Posted { get; set; }

        protected IEngineHost EngineHost
        {
            get { return Setup.EngineHost; }
        }

        protected static DataContext Client { get; set; }

        [SetUp]
        public void BeforeEachTest()
        {
            Client = new DataContext(Setup.EngineHost.Configuration.Get("uri"), new PlatformConfig());

            EngineHost.ExecutionEngine.MessagePosted += (o, request) => Posted.Add(request); ;
        }

        protected bool WasPosted<T>()
        {
            return Posted.Any(x => x.GetType() == typeof(T));
        }
    }
}
