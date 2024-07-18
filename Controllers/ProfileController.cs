using HHD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HHD.Middleware;
using HHD.Service;
using HHD.BL.Auth;
using HHD.BL.Profile;
using HHD.ViewMapper;
using HHD.DAL.Models;

namespace HHD.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;

        public ProfileController(ICurrentUser currentUser, IProfile profile)
        {
            this.currentUser = currentUser;
            this.profile = profile;
        }

        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            var profiles = await currentUser.GetProfiles();
            var profileModel = profiles.FirstOrDefault();

            return View(profileModel != null ? ProfileMapper.MapProfileModelToProfileViewModel(profileModel) : new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            int? userid = await currentUser.GetCurrentUserId();

            if (userid == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userid);

            if (model.ProfileId != null && !profiles.Any(x => x.ProfileId == model.ProfileId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                var profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
                profileModel.UserId = (int)userid;

                await profile.Update(profileModel);
                return Redirect("/");
            }
            
            return View("Index", new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile/uploadimage")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IgameSave(int? profileId)
        {
            int? userid = await currentUser.GetCurrentUserId();

            if (userid == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userid);

            if (profileId != null && !profiles.Any(x => x.ProfileId == profileId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                var profileModel = profiles.FirstOrDefault(x => x.ProfileId == profileId) ?? new ProfileModel();
                profileModel.UserId = (int)userid;

                if (Request.Form.Files.Count > 0 && Request.Form.Files[0] != null)
                {
                    WebFile webFile = new WebFile();
                    string filename = webFile.GetWebFileName(Request.Form.Files[0].FileName);
                    await webFile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), filename, 800, 600);
                    profileModel.ProfileImage = filename;
                    await profile.Update(profileModel);
                }
            }

            return Redirect("/profile");
        }
    }
}
