using HHD.BL.Auth;
using HHD.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using HHD.ViewModels;
using HHD.BL.Data;
using HHD.Service;
using System.Net.Http.Headers;
using HHD.DAL.Models;
using HHD.Models;

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
        [Route("/profile/post")]
        public async Task<IActionResult> Save([FromBody] PostViewModel postview)
        {
            var userId = await currentUser.GetCurrentUserId() ?? 0;
            if (ModelState.IsValid && postview.PostId != null)
            {
                PostModel dbModel = await post.GetPost(postview.PostId ?? 0);
                if (dbModel.UserId != userId)
                {
                    ModelState.TryAddModelError("Title", "Ошибка");
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return new JsonResult(new ErrorsViewModel(ModelState));
                }
            }

            if (postview.PostId == null && postview.ContentItems.Any(m => m.PostContentId != null))
            {
                ModelState.TryAddModelError("Title", "Ошибка");
            }

            if (postview.PostId != null)
            {
                var existingContentIds = (await this.post.GetPostItems(postview.PostId ?? 0)).Where(x => x.PostContentId != null).ToDictionary(x => x.PostContentId ?? 0, x => x.PostId);
            
                if(postview.ContentItems.Any(x => x.PostContentId != null && !existingContentIds.ContainsKey(x.PostContentId ?? 0)))
                {
                    ModelState.TryAddModelError("Title", "Ошибка");
                }
            }

            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(new ErrorsViewModel(ModelState));
            }

            var newModel = ViewMapper.PostMapper.MapPostViewModelToPostModel(postview);
            newModel.UserId = userId;
            int postId = await post.AddOrUpdate(newModel);
            await post.AddOrUpdateContentItems(ViewMapper.PostMapper.MapPostItemViewModelToPostItemModel(postview.ContentItems, postId));
            return new JsonResult(new { id = postId });
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
