using System;
using System.Collections.Generic;
using System.Security.Claims;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Tests.Common
{
    public class UserClaims_Tests
    {
        [Test]
        public void When_Instantiating_From_Claims_Throws_Exception_If_No_Name_Claim()
        {
            Should.Throw<Exception>(() =>
            {
                new UserClaims(new List<Claim>());
            });
        }

        [Test]
        public void When_Instantiating_From_Claims_Only_Requires_Name_Claim()
        {
            var userClaims =  new UserClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "XXX")
            });
            
            userClaims.UserId.ShouldBe("XXX");
            userClaims.Administrate.ShouldBeNull();
            userClaims.View.ShouldBeNull();
            userClaims.Manage.ShouldBeNull();
            userClaims.IsSystemManager.ShouldBeFalse();
        }

        [Test]
        public void When_Instantiating_From_Claims_Populates_All_Properties()
        {
            var userClaims = new UserClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "XXX"),
                new Claim("ParentOrganizationKey", "Z"),
                new Claim("Manage", "A,B,C"),
                new Claim("Administrate", "D,E"),
                new Claim("View", "F,G,H,I"),
                new Claim("IsSystemManager", "True"),
            });

            userClaims.UserId.ShouldBe("XXX");
            userClaims.ParentOrganizationKey.ShouldBe("Z");
            userClaims.IsSystemManager.ShouldBe(true);
            userClaims.View.ShouldBe(new List<string> { "F", "G", "H", "I" });
            userClaims.Administrate.ShouldBe(new List<string> { "D", "E" });
            userClaims.Manage.ShouldBe(new List<string> {"A", "B", "C"});
        }

        [Test]
        public void Can_Generate_Token()
        {
            var userClaims = new UserClaims();
            userClaims.GenerateToken("XXX").ShouldNotBeNull();
        }

        [Test]
        public void Can_Instantiante_From_Token()
        {
            var userClaims = new UserClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "XXX"),
                new Claim("ParentOrganizationKey", "Z"),
                new Claim("Manage", "A,B,C"),
                new Claim("Administrate", "D,E"),
                new Claim("View", "F,G,H,I"),
                new Claim("IsSystemManager", "True"),
            });

            var token = userClaims.GenerateToken("SECRET-KEY");

            var newClaims = new UserClaims("SECRET-KEY", token);

            newClaims.View.ShouldContain(x => x.Contains("F"));
            newClaims.View.ShouldContain(x => x.Contains("G"));
            newClaims.View.ShouldContain(x => x.Contains("H"));
            newClaims.View.ShouldContain(x => x.Contains("I"));
            newClaims.View.Count.ShouldBe(4);

            newClaims.ParentOrganizationKey.ShouldBe("Z");

            newClaims.IsSystemManager.ShouldBeTrue();

        }

        [Test]
        public void Can_Instantiante_From_Token_With_Minimal_Information()
        {
            var userClaims = new UserClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "A")
            });

            var token = userClaims.GenerateToken("XXX");
            var newClaims = new UserClaims("XXX", token);
            newClaims.UserId.ShouldBe("A");
        }
    }
}
