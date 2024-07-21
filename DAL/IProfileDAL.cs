using HHD.DAL.Models;

namespace HHD.DAL
{
    public interface IProfileDAL
    {
        Task<IEnumerable<ProfileModel>> GetByUserId(int userId);
        Task<ProfileModel> GetByProfileId(int profileId);
        Task<int> Add(ProfileModel profile);
        Task Update(ProfileModel profile);
        Task<IEnumerable<ProfileModel>> Search(int top);
    }
}
