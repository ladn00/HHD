using HHD.DAL.Models;

namespace HHD.DAL
{
    public interface IPostDAL
    {
        Task<int> CreatePost(PostModel model);

        Task<PostModel> GetPost(int postid);

        Task<int> UpdatePost(PostModel model);

        Task DeletePost(int postId);


        Task<int> CreatePostContent(PostContentModel model);

        Task<IEnumerable<PostContentModel>> GetPostContent(int postid);

        Task<int> UpdatePostContent(PostContentModel model);

        Task DeletePostContent(int postContentId);
    }
}
