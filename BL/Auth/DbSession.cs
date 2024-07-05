using HHD.DAL.Models;
using HHD.DAL;

namespace HHD.BL.Auth
{
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDAL sessionDAL;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DbSession(IDbSessionDAL sessionDAL, IHttpContextAccessor httpContextAccessor)
        {
            this.sessionDAL = sessionDAL;
            this.httpContextAccessor = httpContextAccessor;
        }

        private void CreateSessionCookie(Guid sessionid)
        {
            CookieOptions options = new CookieOptions();
            options.Path = "/";
            options.HttpOnly = true;
            options.Secure = true;
            httpContextAccessor?.HttpContext?.Response.Cookies.Delete(AuthConstants.SessionCookieName);
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(AuthConstants.SessionCookieName, sessionid.ToString(), options);
        }

        private async Task<SessionModel> CreateSession()
        {
            var data = new SessionModel()
            {
                DbSessionId = Guid.NewGuid(),
                Created = DateTime.Now,
                LastAccessed = DateTime.Now
            };
            await sessionDAL.CreateSession(data);
            return data;
        }

        private SessionModel? sessionModel = null;
        public async Task<SessionModel> GetSession()
        {
            if (sessionModel != null)
                return sessionModel;

            Guid sessionId;
            var cookie = httpContextAccessor?.HttpContext?.Request?.Cookies.FirstOrDefault(m => m.Key == AuthConstants.SessionCookieName);

            if (cookie != null && cookie.Value.Value != null)
                sessionId = Guid.Parse(cookie.Value.Value);
            else
                sessionId = Guid.NewGuid();

            var data = await this.sessionDAL.GetSession(sessionId);
            if (data == null)
            {
                data = await this.CreateSession();
                CreateSessionCookie(data.DbSessionId);
            }
            sessionModel = data;
            return data;
        }

        public async Task<int> SetUserId(int userId)
        {
            var data = await this.GetSession();
            data.UserId = userId;
            data.DbSessionId = Guid.NewGuid();
            CreateSessionCookie(data.DbSessionId);
            return await sessionDAL.CreateSession(data);
        }

        public async Task<int?> GetUserId()
        {
            var data = await this.GetSession();
            return data.UserId;
        }

        public async Task<bool> IsLoggedIn()
        {
            var data = await this.GetSession();
            return data.UserId != null;
        }
    }
}
