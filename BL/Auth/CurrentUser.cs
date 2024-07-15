using HHD.BL.General;
using HHD.BL.Profile;
using HHD.DAL;
using HHD.DAL.Models;

namespace HHD.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IProfileDAL profileDAL;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IDbSession dbSession, IWebCookie webCookie, IUserTokenDAL userTokenDAL, IProfileDAL profileDAL)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
            this.profileDAL = profileDAL;
        }

        public async Task<int?> GetUserByTocken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);

            if (tokenCookie == null)
                return null;

            Guid? token = Helpers.StringToGuidGef(tokenCookie ?? "");
            
            if(token == null) 
                return null;

            int? userid = await userTokenDAL.Get((Guid)token);

            return userid;

        }

        public async Task<bool> IsLoggedIn()
        {
            bool isLoggedIn = await dbSession.IsLoggedIn();
            if (!isLoggedIn)
            {
                int? userid = await GetUserByTocken();
                if(userid != null)
                {
                    await dbSession.SetUserId((int)userid);
                    isLoggedIn = true;
                }
            }
            return isLoggedIn;
        }

        public async Task<int?> GetCurrentUserId()
        {
            return await dbSession.GetUserId();
        }

        public async Task<IEnumerable<ProfileModel>> GetProfiles()
        {
            int? userid = await GetCurrentUserId();

            if (userid == null)
                throw new Exception("Пользователь не найден");

            return await profileDAL.Get((int)userid);
        }
    }
}
