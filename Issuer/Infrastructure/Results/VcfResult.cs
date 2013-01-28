using System.Web.Mvc;
using Issuer.Controllers;
using Issuer.Models;

namespace Issuer.Infrastructure.Results
{
    public class VcfResult : ActionResult
    {
        public Person Person { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write("BEGIN:VCARD\r\n");
            context.HttpContext.Response.Write(string.Format("FN:{0} {1}\r\n", Person.FirstName, Person.LastName));
            context.HttpContext.Response.Write("END:VCARD");
            context.HttpContext.Response.ContentType = "text/directory";
        }
    }
}