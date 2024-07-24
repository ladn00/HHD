using HHD.BL.Auth;
using HHD.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using HHD.ViewModels;
using HHD.BL.Data;

namespace HHD.Controllers
{
    [SiteAuthorize()]
    public class ProfilepostsController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IPost post;

        public ProfilepostsController(ICurrentUser currentUser, IPost post)
        {
            this.currentUser = currentUser;
            this.post = post;

        }

        [HttpGet]
        [Route("/profile/post/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = await currentUser.GetCurrentUserId() ?? 0;
            PostViewModel viewModel = new PostViewModel();

            if (id != 0)
            {
                var postModel = await post.GetPost(id);
                if (postModel == null || postModel.UserId != userId)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    viewModel = ViewMapper.PostMapper.MapPostModelToPostViewModel(postModel);
                }
            }
            return View("Edit", viewModel);
        }
    }
}
