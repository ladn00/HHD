using HHD.BL;
using HHD.BL.Auth;
using HHD.ViewModels;
using HHD.VoewMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
                try
                {
                    await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch(AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверные");
                }
            }

            return View("Index", model);
        }
    }
}
