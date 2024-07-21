using HHD.DAL.Models;
using HHD.DAL;
using HHD.BL.General;
using System.Text.Json;

namespace HHD.BL.Auth
{
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDAL sessionDAL;
        private readonly IWebCookie webCookie;

        private SessionModel? sessionModel = null;
        private Dictionary<string, object> SessionData = new Dictionary<string, object>();

        public DbSession(IDbSessionDAL sessionDAL, IWebCookie webCookie)
        {
            this.sessionDAL = sessionDAL;
            this.webCookie = webCookie;
        }

        private void CreateSessionCookie(Guid sessionid)
        {
            this.webCookie.Delete(AuthConstants.SessionCookieName);
            this.webCookie.AddSecure(AuthConstants.SessionCookieName, sessionid.ToString());
        }

        private async Task<SessionModel> CreateSession()
        {
            var data = new SessionModel()
            {
                DbSessionId = Guid.NewGuid(),
                Created = DateTime.Now,
                LastAccessed = DateTime.Now
            };
            await sessionDAL.Create(data);
            return data;
        }

        public async Task<SessionModel> GetSession()
        {
            if (sessionModel != null)
                return sessionModel;

            Guid sessionId;
            var sessionString = webCookie.Get(AuthConstants.SessionCookieName);

            if (sessionString != null)
                sessionId = Guid.Parse(sessionString);
            else
                sessionId = Guid.NewGuid();

            var data = await this.sessionDAL.Get(sessionId);
            if (data == null)
            {
                data = await this.CreateSession();
                CreateSessionCookie(data.DbSessionId);
            }
            sessionModel = data;
            if (data.SessionData != null)
            {
                SessionData = JsonSerializer.Deserialize<Dictionary<string, object>>(data.SessionData) ?? new Dictionary<string, object>();
            }

            await this.sessionDAL.Extend(data.DbSessionId);
            return data;
        }

        public async Task UpdateSessionData()
        {
            if (this.sessionModel != null)
                await this.sessionDAL.Update(this.sessionModel.DbSessionId, JsonSerializer.Serialize(SessionData));
            else
                throw new Exception("Сессия не загружена");
        }

        public void AddValue(string key, object value)
        {
            if (SessionData.ContainsKey(key))
                SessionData[key] = value;
            else
                SessionData.Add(key, value);
        }

        public void RemoveValue(string key)
        {
            if (SessionData.ContainsKey(key))
                SessionData.Remove(key);
        }

        public object GetValueDef(string key, object defaultValue)
        {
            if (SessionData.ContainsKey(key))
                return SessionData[key];
            return defaultValue;
        }

        public async Task SetUserId(int userId)
        {
            var data = await this.GetSession();
            data.UserId = userId;
            data.DbSessionId = Guid.NewGuid();
            CreateSessionCookie(data.DbSessionId);
            data.SessionData = JsonSerializer.Serialize(SessionData);
            await sessionDAL.Create(data);
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

        public async Task Lock()
        {
            var data = await this.GetSession();
            await sessionDAL.Lock(data.DbSessionId);
        }
    }
}
