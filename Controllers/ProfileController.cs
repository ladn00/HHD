using HHD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;
using HHD.Middleware;

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
            string filename = "";
            var imageData = Request.Form.Files[0];

            MD5 mD5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imageData.FileName);
            byte[] hashBytes = mD5.ComputeHash(inputBytes);
            string hash = Convert.ToHexString(hashBytes);

            var dir = "./wwwroot/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);

            if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            filename =  dir + "/" + imageData.FileName;

            using (var stream = System.IO.File.Create(filename))
            {
                await imageData.CopyToAsync(stream);
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
