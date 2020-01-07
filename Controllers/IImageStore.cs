using System;
using System.IO;
using System.Threading.Tasks;

namespace AzureWebApp_SQL_Service.Controllers
{
    public interface IImageStore
    {
        Task<string> SaveImage(Stream stream, string fileName);
        Uri UriFor(string imageId);
    }
}