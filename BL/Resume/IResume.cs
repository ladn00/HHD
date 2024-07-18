using HHD.DAL.Models;

namespace HHD.BL.Resume
{
    public interface IResume
    {
        Task<IEnumerable<ProfileModel>> Search(int top);

        Task<ResumeModel> Get(int profileId);
    }
}
