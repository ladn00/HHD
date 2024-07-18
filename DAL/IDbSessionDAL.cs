using HHD.DAL.Models;

namespace HHD.DAL
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> Get(Guid sessionId);

        Task Update(SessionModel model);

        Task Create(SessionModel model);

        Task Lock(Guid sessionId);
    }
}
