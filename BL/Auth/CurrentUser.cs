namespace HHD.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
                this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsLoggedIn()
        {
            int? id = httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME);
            return id != null;
        }
    }
}
