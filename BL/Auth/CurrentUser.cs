using HHD.BL.General;
using HHD.DAL;

namespace HHD.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IDbSession dbSession, IWebCookie webCookie, IUserTokenDAL userTokenDAL)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
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
    }
}
