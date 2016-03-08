using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Client;

namespace Trackwane.Framework.Tests.Client
{
    internal class ContextClientTests
    {
        public class TestModel 
        {
            public string MyProperty { get; set; }

            public bool MyBooleanProperty { get; set; }

            public DateTime MyDateTimeProperty { get; set; }
        }

        [Test]
        public void GET_Creates_Querystring_For_Model_When_Provided()
        {
            var result = RequestBuilder.GET("world-latin-america-35514811", new TestModel
            {
                MyProperty = "MyPropertyValue",
                MyBooleanProperty = true,
                MyDateTimeProperty = new DateTime(2012, 1, 4, 4, 32, 29)
            });

           result.Parameters[0].Name.ShouldBe("MyProperty");
           result.Parameters[1].Name.ShouldBe("MyBooleanProperty");
           result.Parameters[2].Name.ShouldBe("MyDateTimeProperty");

            result.Parameters[0].Value.ShouldBe("MyPropertyValue");
            result.Parameters[1].Value.ShouldBe("True");
            result.Parameters[2].Value.ShouldBe("2012-01-04T04:32:29.0000000");
        }
    }
}
