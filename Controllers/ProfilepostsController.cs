using HHD.BL.Auth;
using HHD.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using HHD.ViewModels;
using HHD.BL.Data;
using HHD.Service;
using System.Net.Http.Headers;

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

        [HttpGet]
        [Route("/profile/postdata/{id}")]
        public async Task<IActionResult> postdata(int id)
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
            return new JsonResult(viewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Save([FromBody] PostViewModel model)
        {
            return View();
        }

        [HttpPost]
        [Route("/profile/post/image")]
        public async Task<IActionResult> UplaodImage()
        {
            int? userid = await currentUser.GetCurrentUserId();
            WebFile webFile = new WebFile();
            string filename = webFile.GetWebFileName(userid + "-" + Request.Form.Files[0].FileName, "postimages");
            await webFile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), filename, 800, 600);
            return new JsonResult(new { Filename = filename });
        }
    }
}
