using HHD.BL.Auth;
using HHD.ViewModels;
using HHD.VoewMapper;
using Microsoft.AspNetCore.Mvc;

namespace HHD.Controllers
{
    public class LoginController : Controller
    {
        public readonly IAuthBL authBl;

        public LoginController(IAuthBL authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                return Redirect("/");
            }

            return View("Index", model);
        }
    }
}
