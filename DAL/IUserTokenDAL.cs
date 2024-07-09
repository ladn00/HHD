using HHD.DAL.Models;

namespace HHD.DAL
{
    public interface IUserTokenDAL
    {
        Task<Guid> Create(int userid);

        Task<int?> Get(Guid tokenid);
    }
}
