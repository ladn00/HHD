using HHD.DAL.Models;

namespace HHD.DAL
{
    public interface IProfileDAL
    {
        Task<IEnumerable<ProfileModel>> Get(int userId);
        Task<int> Add(ProfileModel model);
        Task Update(ProfileModel model);
    }
}
