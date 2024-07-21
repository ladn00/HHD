using HHD.DAL.Models;

namespace HHD.DAL
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> Get(Guid sessionId);

        Task Update(Guid dbSessionID, string sessionData);

        Task Extend(Guid dbSessionID);

        Task Create(SessionModel model);

        Task Lock(Guid sessionId);
    }
}
