using System;
using System.Collections.Generic;
using System.Linq;
using Issuer.Infrastructure;
using Issuer.Models;
using Raven.Client;

namespace Issuer.Repositories
{
    public class IssuesRepository : IIssuesRepository
    {
        private readonly IDocumentSession _session;

        public IssuesRepository(IDocumentSession session)
        {
            _session = session;
        }

        public IList<Issue> GetAll()
        {
            return _session.Query<Issue>().ToList();
        }

        public void Save(Issue issue)
        {
            _session.Store(issue);
            _session.SaveChanges();
        }

        public Issue GetById(string id)
        {
            return _session.Load<Issue>(id);
        }

        public void Delete(Issue issue)
        {
            _session.Delete(issue);
            _session.SaveChanges();
        }
    }
}