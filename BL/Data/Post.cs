using HHD.DAL.Models;
using HHD.DAL;

namespace HHD.BL.Data
{
    public class Post : IPost
    {
        private readonly IPostDAL postDAL;

        public Post(IPostDAL postDAL)
        {
            this.postDAL = postDAL;
        }

        public async Task<PostModel> GetPost(int postId)
        {
            return await postDAL.GetPost(postId);
        }

        public async Task<int> AddOrUpdate(PostModel model)
        {
            model.Modified = DateTime.Now;

            if (model.PostId == null)
            {
                model.Created = DateTime.Now;
                return await postDAL.CreatePost(model);
            }
            else
            {
                await postDAL.UpdatePost(model);
                return model.PostId ?? 0;
            }
        }

        public async Task AddOrUpdateContentItems(IEnumerable<PostContentModel> items)
        {
            foreach (PostContentModel model in items)
            {
                if (model.PostContentId == null)
                {
                    model.PostContentId = await postDAL.CreatePostContent(model);
                }
                else
                {
                    await postDAL.UpdatePostContent(model);
                }
            }
        }

        public async Task<List<PostContentModel>> GetPostItems(int postId)
        {
            var result = await postDAL.GetPostContent(postId);
            return result.ToList();
        }
    }
}
