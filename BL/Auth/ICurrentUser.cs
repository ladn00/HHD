using HHD.DAL.Models;

namespace HHD.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<int?> GetCurrentUserId();
        Task<IEnumerable<ProfileModel>> GetProfiles();
    }
}
