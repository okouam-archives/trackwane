using System.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common;
using Trackwane.Framework.Web.Security;

namespace Trackwane.Framework.Tests.Web
{
    internal class Secured_Tests
    {
        private readonly string secretKey = ConfigurationManager.AppSettings["platform:secretKey"];

        [Test]
        public void Runs_To_Completion_When_No_Token_Authentication_Used()
        {
            var filter = new SecuredAttribute();

            var context = MockHttpAuthenticationContext();

            var taskResult = filter.AuthenticateAsync(context, new CancellationToken());

           taskResult.Status.ShouldBe(TaskStatus.RanToCompletion);
        }

        [Test]
        public void Sets_Claims_Principal_When_Token_Authentication_Used()
        {
            var userClaims = new UserClaims {UserId = "A"};

            var message = new HttpRequestMessage
            {
                Headers = {{"Authorization", $"Bearer {userClaims.GenerateToken(secretKey)}"}}
            };

            var context = MockHttpAuthenticationContext(message);

            new SecuredAttribute().AuthenticateAsync(context, new CancellationToken());
            
            var principal = context.Principal as ClaimsPrincipal;
            principal.ShouldNotBeNull();
            principal.Identity.Name.ShouldBe("A");
        }

        private static HttpAuthenticationContext MockHttpAuthenticationContext(HttpRequestMessage message = null)
        {
            var context = new HttpAuthenticationContext(
                new HttpActionContext(
                    new HttpControllerContext(
                        new HttpRequestContext(), message ?? new HttpRequestMessage(), new HttpControllerDescriptor(),
                        new BaseApiController()
                        ),
                    new ReflectedHttpActionDescriptor()
                    ),
                new GenericPrincipal(
                    new GenericIdentity("test"), null));
            return context;
        }
    }
}
