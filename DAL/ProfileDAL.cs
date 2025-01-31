﻿using HHD.BL.Profile;
using HHD.DAL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HHD.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(ProfileModel model)
        {
            string sql = @"insert into Profile(UserId, ProfileName, FirstName, LastName, ProfileImage, ProfileStatus)
                                values(@UserId, @ProfileName, @FirstName, @LastName, @ProfileImage, @ProfileStatus) returning ProfileID"
            ;

            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@"
                        select ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage, ProfileStatus
                        from Profile 
                        Where UserId = @userId", new { userId = userId });
        }

        public async Task Update(ProfileModel model)
        {
            string sql = @"update Profile
                      set ProfileName = @ProfileName, FirstName = @FirstName, LastName = @LastName, ProfileImage = @ProfileImage, ProfileStatus = @ProfileStatus
                      where ProfileId = @ProfileId";

            await DbHelper.ExecuteAsync(sql, model);
        }

        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@$"
                        select ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage
                        from Profile
                        where profilestatus = @profileStatus
                        order by 1 desc
                        limit @top 
                        ", new { top = top, profileStatus = ProfileStatusEnum.Public });
        }

        public async Task<ProfileModel> GetByProfileId(int profileId)
        {
            return await DbHelper.QueryScalarAsync<ProfileModel>(@"
                        select 	ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage, ProfileStatus
                        from Profile
                        where ProfileId = @profileId", new { profileId = profileId });
        }

        public async Task<IEnumerable<ProfileModel>> GetByUserId(int userId)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@"
                        select 	ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage 
                        from Profile
                        where UserId = @id", new { id = userId });
        }
    }
}
