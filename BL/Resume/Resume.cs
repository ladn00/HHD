using HHD.DAL.Models;
using HHD.DAL;

namespace HHD.BL.Resume
{
    public class Resume : IResume
    {
        private readonly IProfileDAL profileDAL;

        public Resume(IProfileDAL profileDAL)
        {
            this.profileDAL = profileDAL;
        }

        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await profileDAL.Search(top);
        }

        public async Task<ResumeModel> Get(int profileId)
        {
            ProfileModel profileModel = await this.profileDAL.GetByProfileId(profileId);
            return new ResumeModel()
            {
                Profile = profileModel
            };
        }
    }
}
