using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Issuer.Infrastructure
{
    public static class HtmlHelpersExtensions
    {
        public static MvcHtmlString ValidationSummaryAlerts(this HtmlHelper helper, ModelStateDictionary modelState)
        {
            var alerts = new StringBuilder();

            foreach (var error in modelState.Values.SelectMany(e => e.Errors))
            {
                alerts.Append(string.Format("<div class=\"alert alert-error\">{0}</div>", error.ErrorMessage));
            }

            return new MvcHtmlString(alerts.ToString());
        }
    }
}