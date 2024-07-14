using HHD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;
using HHD.Middleware;
using System.Text;
using HHD.Service;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HHD.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave()
        {
            var imageData = Request.Form.Files[0];

            if (imageData != null)
            {
                WebFile webFile = new WebFile();
                string filename = webFile.GetWebFileName(imageData.FileName);
                await webFile.UploadAndResizeImage(imageData.OpenReadStream(), filename, 800, 600);
            }
            return View("Index", new ProfileViewModel());
        }
    }
}
