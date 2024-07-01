using HHD.DAL.Models;

namespace HHD.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel user);
        Task<int> Authenticate(string email, string password, bool rememberMe);
    }
}
