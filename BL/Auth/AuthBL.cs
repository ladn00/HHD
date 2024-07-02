using HHD.DAL.Models;
using HHD.DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HHD.BL;

namespace HHD.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthBL(IAuthDAL authDal, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor) 
        { 
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(email);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                Login(user.UserId ?? 0);
                return user.UserId ?? 0;
            }

            throw new AuthorizationException();
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDal.CreateUser(user);
            Login(id);
            return id;
        }

        public void Login(int id)
        {
            httpContextAccessor.HttpContext?.Session.SetInt32("userid", id);
        }

        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            var user = await authDal.GetUser(email);

            if (user.UserId != null)
                return new ValidationResult("Email уже существует");
            return null;
        }
    }
}
