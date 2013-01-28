using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests.Asp;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using Issuer.Controllers;
using Issuer.Repositories;
using NUnit.Framework;

namespace Issuer.Tests.Approvals
{
    [UseReporter(typeof(FileLauncherReporter))]
    public class ApprovalTests
    {
        [Test]
        public void all_issues_test()
        {
            AspApprovals.VerifyUrl("http://localhost:51989/issues/all");
        }

        [Test]
        public void by_id_test()
        {
            AspApprovals.VerifyUrl("http://localhost:51989/issues/show/issues-1");
        }
    }
}
