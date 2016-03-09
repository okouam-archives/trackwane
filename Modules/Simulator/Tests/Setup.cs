using log4net.Config;
using NUnit.Framework;

namespace Trackwane.Simulator.Tests
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void BeforeAll()
        {
            XmlConfigurator.Configure();
        }
    }
}

