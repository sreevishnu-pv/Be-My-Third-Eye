using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ThirdEye.Services.Services
{
    public class DocumentDBService
    {
        private static string docDbUri = ConfigurationManager.AppSettings["DocDBUri"];
       // private static string docDbNameConnect = ConfigurationManager.AppSettings["DocDBNameConnect"];

        public void InsertDocument(string story, List<string> links, List<string> videoLinks, string location, string userName = "ThirdEyeSystem")
        {
            dynamic doc1Definition = new
            {

                userId = userName,
                story = story,
                links = links,
                videos = videoLinks,
                date = DateTime.Now,
                location = location
            };

            using (var client = new DocumentClient(new Uri(docDbUri), "SonnmG6zefrFr8vo3bzOkPFYouyv4aOnjPaWHfNJj1JIKPK2pidYbCdnNsULbjF6S6K30bL6Uio5HwWN42gFhw=="))
            {
                var dbb = client.CreateDatabaseQuery()
                    .Where(d => d.Id == "Infrastructure")
                    .AsEnumerable()
                    .FirstOrDefault();

                var allCollections = client.CreateDocumentCollectionQuery(dbb.SelfLink).ToList();
                var coll = allCollections
                            .Where(c => c.Id == "UserDetails").AsEnumerable()
                            .FirstOrDefault();

                var document1 = client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbb.Id, coll.Id), doc1Definition).Result;
            }
        }
    }
}
