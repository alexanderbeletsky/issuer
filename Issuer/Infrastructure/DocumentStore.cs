using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Embedded;

namespace Issuer.Infrastructure
{
    public class DocumentStore
    {
        private static EmbeddableDocumentStore _documentStore;

        public static void Initialize()
        {
            _documentStore = new EmbeddableDocumentStore
                                 {
                                     DataDirectory = "~/App_Data/db2",
                                     Conventions = {IdentityPartsSeparator = "-"}
                                 };

            _documentStore.Initialize();
        }

        public static IDocumentStore Store
        {
            get
            {
                return _documentStore;
            }
        }

        public static IDocumentSession Session
        {
            get { return _documentStore.OpenSession(); }
        }
    }
}