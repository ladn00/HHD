﻿using HHD.BL.Auth;
using HHD.ViewModels;
using HHD.VoewMapper;
using Microsoft.AspNetCore.Mvc;

namespace HHD.Controllers
{
    public class RegisterController : Controller
    {
        public readonly IAuthBL authBl;

        public RegisterController(IAuthBL authBl)
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
        public IActionResult IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                authBl.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }

            return View("Index", new RegisterViewModel());
        }
    }
}