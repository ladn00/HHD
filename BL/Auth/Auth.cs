using HHD.DAL.Models;
using HHD.DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HHD.BL;
using HHD.BL.General;
using System.Reflection.Metadata;

namespace HHD.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IDbSession dbSession;

        public Auth(IAuthDAL authDal, IEncrypt encrypt, IWebCookie webCookie, IUserTokenDAL userTokenDAL, IDbSession dbSession) 
        { 
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
            this.dbSession = dbSession;
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(email);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId ?? 0);

                if(rememberMe)
                {
                    var tockenid = await userTokenDAL.Create(user.UserId ?? 0);
                    webCookie.AddSecure(AuthConstants.RememberMeCookieName, tockenid.ToString(), 30);
                }

                return user.UserId ?? 0;
            }

            throw new AuthorizationException();
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDal.CreateUser(user);
            await Login(id);
            return id;
        }

        public async Task Register(UserModel user)
        {
            using (var scope = General.Helpers.CreateTransactionScope())
            {
                await dbSession.Lock();
                await ValidateEmail(user.Email);
                await CreateUser(user);
                scope.Complete();
            } 
        }

        public async Task Login(int id)
        {
            await dbSession.SetUserId(id);
        }

        public async Task ValidateEmail(string email)
        {
            var user = await authDal.GetUser(email);

            if (user.UserId != null)
                throw new DuplicateEmailException();
        }
    }
}
