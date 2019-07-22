using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Net;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Linq;


namespace DocDB
{
    public class Program
    {
        private static readonly string urname = "https://platforminfra.documents.azure.com:443/";
        // The primary key for the Azure Cosmos account.
        private static readonly string myname = "SonnmG6zefrFr8vo3bzOkPFYouyv4aOnjPaWHfNJj1JIKPK2pidYbCdnNsULbjF6S6K30bL6Uio5HwWN42gFhw==";

        static void Main(string[] args)
        {
            Console.WriteLine("\r\n>>>>>>>>>>>>>>>> Creating Document <<<<<<<<<<<<<<<<<<<");
            
            dynamic doc1Definition = new
            {
                userId = "imahmed",
                story = "The three-day Shakhambari Devi Festival at the Sri Durga Malleswara Swamy Varla Devasthanam (Kanakadurga Temple) atop Indrakeeladri began in a grand manner on Wednesday.Devotees started thronging the temple from five in the morning to have a darshan of the presiding deity adorning a variety of vegetables.The temple was decked up with a special arrangement of vegetables all over.However, the ongoing strike by truckers across the country had a severe impact on the festival.The Devasthanam authorities decorate the temple using at least 30 tonnes of vegetables every year on the first day.On Wednesday, only 12 tonnes of vegetables were used owing to short supply due to the ongoing nationwide strike by truckers, according to officials. Only a few vegetables were used to decorate the sanctum sanctorum instead of using a variety of vegetables due to scarcity.The temple has been celebrating the festival since 2007 seeking abundant rains and proper harvest of vegetables subsequently.A variety of green vegetables adorns the passage to Sri Durga Malleswara Swamy Varla Devasthanam on the first day of Shakambari Devi festival, in Vijayawada on Wednesday.",
                links = new List<string>() { @"https:\/\/www.thehindu.com\/news\/cities\/Vijayawada\/shakhambari-devi-festival-begins\/article24514788.ece",
                    @"http:\/\/kanakadurgamma.org\/",@"http:\/\/www.hindutourism.com\/browse-all-61-39\/gallery\/sri-durga-malleswara-swamy-varla-devastanams-vijayawada"  },
                videos=new List<string>() { @"https:\/\/www.youtube.com\/watch?v=ERpfkL7T7mk", @"https:\/\/www.youtube.com\/watch?v=-xSTCf2TjcA" },
                date = "22nd July, 2019",
                location = "Sri Durga Malleswara Swamy Varla Devasthanam, Vijaywada"
            };
            Task.Run(async () =>
            {
                
                using (var client = new DocumentClient(new Uri(urname), myname))
                {
                    var dbb = client.CreateDatabaseQuery().Where(d => d.Id == "Infrastructure").AsEnumerable().FirstOrDefault();
                    var allCollections = client.CreateDocumentCollectionQuery(dbb.SelfLink).ToList();
                    var coll = allCollections
                                .Where(c => c.Id == "UserDetails").AsEnumerable()
                                .FirstOrDefault();
                    var document1 = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbb.Id, coll.Id), doc1Definition);
                }

            }).Wait();
           
        }

        


        public  void  Add()
        {
            
        }

    } 
    
}
