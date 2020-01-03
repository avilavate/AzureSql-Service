using AzureWebApp_SQL_Service.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureWebApp_SQL_Service
{
    public class CourseStore
    {
        private DocumentClient client;
        private Uri courseLink;
        private readonly string DatabaseId = "azurehellocosmosdb";
        private readonly string CollectionId = "courses";
        public CourseStore()
        {
            Uri uri = new Uri("https://azurehellocosmosdb.documents.azure.com:443/");
            var key = "OiZesGL9ocWIiiAtgSlNtjJkT13w9F9hHWC9yM5wUcJ9EKHs4BmjYGDexquR8aJzQQmpMCCdvvLMP0kffZSQbg==";
            client = new DocumentClient(uri, key);
            courseLink = UriFactory.CreateDocumentCollectionUri("azurehellocosmosdb", "courses");


            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public async Task InsetCourse(IEnumerable<CourseModel > Courses)
        {
            foreach(var course in Courses)
            {
               await client.CreateDocumentAsync(courseLink, course);
            }
        }

        public IEnumerable<CourseModel> GetAllCourses()
        {
            var courses= client.CreateDocumentQuery<CourseModel>(courseLink)
                  .OrderBy(c => c.Name);
            return courses;
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }
        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
