using log4net.Config;
using NUnit.Framework;

[SetUpFixture]
public class Setup
{
    [OneTimeSetUp]
    public void BeforeAll()
    {
        XmlConfigurator.Configure();
    }
}

