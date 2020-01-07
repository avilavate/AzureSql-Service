using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureWebApp_SQL_Service.Controllers
{
    public class ImageController : Controller
    {
        private ImageStore imageStore;

        public ImageController(IImageStore imageStore)
        {
            this.imageStore = new ImageStore();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile Image)
        {
            if (Image != null)
            {
                using(var stream = Image.OpenReadStream())
                {
                    var imageId = this.imageStore.SaveImage(stream, Image.FileName);
                    return RedirectToAction("Show", new { imageId });
                }
            }
            return View("Index");
        }

        public IActionResult Show(string imageId)
        {
            var model = new ShowModel { URI = this.imageStore.UriFor(imageId) };
            return View(model);
        }
    }
}