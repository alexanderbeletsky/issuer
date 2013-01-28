using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Issuer.Controllers;
using Issuer.Models;
using Issuer.Providers;
using Issuer.Repositories;
using NSubstitute;
using NUnit.Framework;

// should_all_return_all_existing_issues
// should_show_no_issues_view_if_issues_list_is_empty
// should_return_empty_model_for_new
// should_return_issue_with_created_at_intializied_to_current_date_time
// should_store_new_issue_to_repository
// should_redirect_to_all_issues
// should_return_view_back_if_model_in_invalid_state
// should_show_return_issue_by_given_id
// should_return_404_if_issue_with_given_id_does_not_exist
// should_complete_return_are_you_sure_view
// should_complete_update_complete_flad_and_store
// should_complete_redirect_to_all
// should_delete_issue_by_given_id
// should_delete_redirect_to_all
// should_delete_return_404_if_issue_with_given_id_not_found

namespace Issuer.Tests.Controllers
{
    public class IssuesControllerTests
    {
        private IssuesController _controller;
        private IIssuesRepository _issuesRepository;
        private IDateTimeProvider _datetimeProvider;

        [SetUp]
        public void Setup()
        {
            // dependencies
            _datetimeProvider = Substitute.For<IDateTimeProvider>();
            _issuesRepository = Substitute.For<IIssuesRepository>();
   
            // system under test
            _controller = new IssuesController(_issuesRepository, _datetimeProvider);
        }

        [Test]
        public void should_return_all_registered_issues()
        {
            // arrange
            var issues = new List<Issue>
                             {
                                 new Issue {Headline = "Head", Description = "Hi" }
                             };

            _issuesRepository.GetAll().Returns(issues);

            // act
            var result = _controller.All() as ViewResult;

            // assert
            var model = result.Model as IList<Issue>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model, Is.EqualTo(issues));
        }

        [Test]
        public void should_return_no_issues_if_no_issues_registered_yet()
        {
            // arrage
            _issuesRepository.GetAll().Returns(new List<Issue>());

            // act
            var result = _controller.All() as ViewResult;

            // assert
            Assert.That(result.ViewName, Is.EqualTo("NoIssues"));
        }

        [Test]
        public void should_return_empty_view_for_new_issue()
        {
            // act
            var result = _controller.New() as ViewResult;

            // assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void should_return_not_initialized_model()
        {
            // act
            var result = _controller.New() as ViewResult;

            // assert
            var model = result.Model as Issue;
            Assert.That(model.Headline, Is.Null);
            Assert.That(model.Description, Is.Null);
            Assert.That(model.Completed, Is.False);
        }

        [Test]
        public void should_new_return_model_with_created_date_as_current_date()
        {
            // arrange
            var currentDate = DateTime.Now;
            _datetimeProvider.Now.Returns(currentDate);

            // act
            var result = _controller.New() as ViewResult;

            // assert
            var model = result.Model as Issue;
            Assert.That(model.CreatedAt, Is.EqualTo(currentDate));
        }

        [Test]
        public void should_save_newly_create_issues()
        {
            // arrange
            var issue = new Issue
                            {
                                Headline = "Headline",
                                Description = "Lol"
                            };

            // act
            _controller.New(issue);

            // assert
            _issuesRepository.Received().Save(issue);
        }

        [Test]
        public void should_new_post_redirect_to_all_issues_after_save()
        {
            // arrange
            var issue = new Issue
            {
                Headline = "Headline",
                Description = "Lol"
            };

            // act
            var result = _controller.New(issue) as RedirectToRouteResult;

            // assert
            Assert.That(result.RouteValues["action"], Is.EqualTo("all"));
        }

        [Test]
        public void should_new_post_return_view_with_model_if_post_data_is_invalid()
        {
            // arrange
            var issue = new Issue
            {
                Headline = "Headline",
                Description = "Lol"
            };
            _controller
                .ModelState
                .AddModelError("Headline", "Headline is required");
            
            var result = _controller.New(issue) as ViewResult;

            // assert
            Assert.That(result.Model, Is.EqualTo(issue));
        }
    }
}
