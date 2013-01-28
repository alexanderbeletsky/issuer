using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Issuer.Infrastructure;
using Issuer.Infrastructure.Results;
using Issuer.Models;
using Issuer.Providers;
using Issuer.Repositories;

namespace Issuer.Controllers
{
    [IssuerAuthorize]
    public class IssuesController : Controller
    {
        private readonly IIssuesRepository _issuesRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public IssuesController(IIssuesRepository issuesRepository, IDateTimeProvider dateTimeProvider)
        {
            _issuesRepository = issuesRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public ActionResult All()
        {
            var issues = _issuesRepository.GetAll();

            if (!issues.Any())
            {
                return View("NoIssues");
            }

            return View(issues);
        }

        public ActionResult New()
        {
            var issue = new Issue
                            {
                                CreatedAt = _dateTimeProvider.Now
                            };

            return View(issue);
        }

        [HttpPost]
        public ActionResult New(Issue issue)
        {
            if (ModelState.IsValid)
            {
                _issuesRepository.Save(issue);
                return RedirectToAction("all");
            }

            return View(issue);
        }

        public ActionResult Show(string id)
        {
            var issue = _issuesRepository.GetById(id);

            if (issue == null)
            {
                return new HttpNotFoundResult();
            }

            return View(issue);
        }

        [HttpGet]
        public ActionResult Complete(string id)
        {
            var issue = _issuesRepository.GetById(id);

            return View(issue);
        }

        [HttpPost]
        [ActionName("Complete")]
        public ActionResult CompleteIssue(string id)
        {
            var issue = _issuesRepository.GetById(id);

            if (issue == null)
            {
                return new HttpNotFoundResult();
            }

            issue.Completed = true;
            _issuesRepository.Save(issue);

            return RedirectToAction("all");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var issue = _issuesRepository.GetById(id);

            if (issue == null)
            {
                return new HttpNotFoundResult();
            }

            _issuesRepository.Delete(issue);

            return RedirectToAction("all");
        }

        [HttpGet]
        public ActionResult Csv()
        {
            var issues = _issuesRepository.GetAll();
            var csvContent = new StringBuilder();

            foreach (var issue in issues)
            {
                csvContent.Append(string.Format("{0};{1};{2}\r\n", issue.Id, issue.Headline, issue.Description));
            }

            return new ContentResult
                       {
                           Content = csvContent.ToString(),
                           ContentType = "application/excel"
                       };
        }

        [HttpGet]
        public ActionResult File()
        {
            var issues = _issuesRepository.GetAll();
            var csvContent = new StringBuilder();

            foreach (var issue in issues)
            {
                csvContent.Append(string.Format("{0};{1};{2}\r\n", issue.Id, issue.Headline, issue.Description));
            }


            var stream = new MemoryStream(Encoding.ASCII.GetBytes(csvContent.ToString()));

            return new FileStreamResult(stream, "application/excel")
                       {
                           FileDownloadName = "exported.csv"
                       };
        }

        [HttpGet]
        public ActionResult Json(string id)
        {
            var issue = _issuesRepository.GetById(id);

            return new JsonNetResult(issue);
        }
    }
}
