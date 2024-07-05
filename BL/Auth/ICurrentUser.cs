namespace HHD.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
    }
}
