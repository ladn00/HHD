using HHD.DAL.Models;

namespace HHD.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(ProfileModel model)
        {
            string sql = @"insert into Profile(UserId, ProfileName, FirstName, LastName, ProfileImage)
                                values(@UserId, @ProfileName, @FirstName, @LastName, @ProfileImage) returning ProfileID";

            var result = await DbHelper.QueryAsync<int>(sql, model);
            return result.First();
        }

        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@"
                        select ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage
                        from Profile 
                        Where UserId = @userId", new { userId = userId });
        }

        public async Task Update(ProfileModel model)
        {
            string sql = @"update Profile
                      set ProfileName = @ProfileName, FirstName = @FirstName, LastName = @LastName, ProfileImage = @ProfileImage
                      where ProfileId = @ProfileId";

            await DbHelper.QueryAsync<int>(sql, model);
        }
    }
}
