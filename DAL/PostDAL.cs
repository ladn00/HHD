using HHD.DAL.Models;

namespace HHD.DAL
{
    public class PostDAL : IPostDAL
    {
        public async Task<int> CreatePost(PostModel model)
        {
            string sql = @"insert into Post (UserId, UniqueId, Title, Intro, Created, Modified, Status)
                    values (@UserId, @UniqueId, @Title, @Intro, @Created, @Modified, @Status)
                    returning PostId";

            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<PostModel> GetPost(int postId)
        {
            string sql = @"select * from Post where PostId = @postId";
            return await DbHelper.QueryScalarAsync<PostModel>(sql, new { postId = postId });
        }

        public async Task DeletePost(int postId)
        {
            string sql = @"delete from PostContent where PostId = @postId";
            await DbHelper.ExecuteAsync(sql, new { postId = postId });

            sql = @"delete from Post where PostId = @postId";
            await DbHelper.ExecuteAsync(sql, new { postId = postId });
        }

        public async Task<int> UpdatePost(PostModel model)
        {
            string sql = @"update Post 
                    set Title = @Title, 
                        Intro = @Intro, 
                        Status = @Status,
                        Modified = @Modified
                    where PostId = @PostId";

            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<int> CreatePostContent(PostContentModel model)
        {
            string sql = @"insert into PostContent (PostId, ContentItemType, Value)
                    values (@PostId, @ContentItemType, @Value)
                    returning PostContentId";

            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<PostContentModel>> GetPostContent(int postId)
        {
            string sql = @"select * from PostContent where PostId = @postId";
            return await DbHelper.QueryAsync<PostContentModel>(sql, new { postId = postId });
        }

        public async Task<int> UpdatePostContent(PostContentModel model)
        {
            string sql = @"update PostContent 
                    set ContentItemType = @ContentItemType, 
                        Value = @Value
                    where PostContentId = @PostContentId";

            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task DeletePostContent(int postContentId)
        {
            string sql = @"delete from PostContent where PostContentId = @postContentId";
            await DbHelper.ExecuteAsync(sql, new { postContentId = postContentId });
        }
    }
}
