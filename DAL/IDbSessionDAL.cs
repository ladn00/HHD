using HHD.DAL.Models;

namespace HHD.DAL
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> GetSession(Guid sessionId);

        Task<int> UpdateSession(SessionModel model);

        Task<int> CreateSession(SessionModel model);
    }
}
