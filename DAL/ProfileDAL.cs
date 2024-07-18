using HHD.BL.Profile;
using HHD.DAL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HHD.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(ProfileModel model)
        {
            string sql = @"insert into Profile(UserId, ProfileName, FirstName, LastName, ProfileImage)
                                values(@UserId, @ProfileName, @FirstName, @LastName, @ProfileImage) returning ProfileID"
            ;

            return await DbHelper.QueryScalarAsync<int>(sql, model);
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

            await DbHelper.ExecuteAsync(sql, model);
        }
    }
}
