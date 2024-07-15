using HHD.DAL;
using HHD.DAL.Models;

namespace HHD.BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDAL profileDAL;

        public Profile(IProfileDAL profileDAL)
        {
            this.profileDAL = profileDAL;  
        }

        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await profileDAL.Get(userId);
        }

        public async Task Update(ProfileModel profileModel)
        {
            if(profileModel.ProfileId == null)
                await profileDAL.Add(profileModel);
            else
                await profileDAL.Update(profileModel);
        }
    }
}
