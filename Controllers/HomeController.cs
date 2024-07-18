using HHD.BL.Auth;
using HHD.BL.Resume;
using HHD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HHD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICurrentUser currentUser;
        private readonly IResume resume;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser, IResume resume)
        {
            this.logger = logger;
            this.currentUser = currentUser;
            this.resume = resume;
        }

        public async Task<IActionResult> Index()
        {
            var latestResumes = await resume.Search(4);
            return View(latestResumes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
