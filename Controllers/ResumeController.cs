using HHD.BL.Auth;
using HHD.BL.Profile;
using HHD.BL.Resume;
using HHD.DAL.Models;
using HHD.Middleware;
using HHD.Models;
using HHD.ViewMapper;
using HHD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HHD.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IResume resume;

        public ResumeController(IResume resume)
        {
            this.resume = resume;
        }

        [Route("/resume/{profileid}")]
        public async Task<IActionResult> Index(int profileid)
        {
            var model = await resume.Get(profileid);
            return View(model);
        }
    }
}
