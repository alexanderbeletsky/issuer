using System.Web;
using System.Web.Mvc;
using Issuer.Repositories;
using NSubstitute;
using NUnit.Framework;
using Ninject;

namespace Issuer.Infrastructure
{
    public class IssuerAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.UserAgent != null && filterContext.HttpContext.Request.UserAgent.Contains("IE"))
            {
                filterContext.Result =  new HttpUnauthorizedResult();
            }
        }

        [Inject]
        public IIssuesRepository Repository
        {
            get;
            set;
        }
    }

    public class IssuerAuthorizeTests
    {
        [Test]
        public void should_reject_ie_users()
        {
            // arrange
            var filterContext = new AuthorizationContext();
            var request = Substitute.For<HttpRequestBase>();
            filterContext.HttpContext = Substitute.For<HttpContextBase>();
            filterContext.HttpContext.Request.Returns(request);
            filterContext.HttpContext.Request.UserAgent.Returns("IE");
            var filter = new IssuerAuthorize();
            
            // act
            filter.OnAuthorization(filterContext);

            // assert
            Assert.That(filterContext.Result, Is.TypeOf<HttpUnauthorizedResult>());
        }

    }
}