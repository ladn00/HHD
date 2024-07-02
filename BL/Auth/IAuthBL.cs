using HHD.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace HHD.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel user);
        Task<int> Authenticate(string email, string password, bool rememberMe);
        Task<ValidationResult?> ValidateEmail(string email);
    }
}
