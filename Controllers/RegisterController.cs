using HHD.BL.Auth;
using HHD.ViewModels;
using HHD.VoewMapper;
using Microsoft.AspNetCore.Mvc;

namespace HHD.Controllers
{
    public class RegisterController : Controller
    {
        public readonly IAuth authBl;

        public RegisterController(IAuth authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (HHD.BL.DuplicateEmailException) 
                {
                    ModelState.TryAddModelError("Email", "Email уже существует");
                }
                
            }

            return View("Index", model);
        }
    }
}
