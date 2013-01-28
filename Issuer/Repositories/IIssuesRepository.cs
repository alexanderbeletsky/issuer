using System.Collections.Generic;
using Issuer.Models;

namespace Issuer.Repositories
{
    public interface IIssuesRepository
    {
        IList<Issue> GetAll();
        void Save(Issue issue);
        Issue GetById(string id);
        void Delete(Issue issue);
    }
}