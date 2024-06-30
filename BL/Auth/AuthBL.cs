using HHD.DAL.Models;
using HHD.DAL;

namespace HHD.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL authDal;

        public AuthBL(IAuthDAL authDal) 
        { 
            this.authDal = authDal;
        }

        public async Task<int> CreateUser(UserModel user)
        {
            return await authDal.CreateUser(user);
        }
    }
}
