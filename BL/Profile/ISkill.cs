using HHD.DAL.Models;

namespace HHD.BL.Profile
{
    public interface ISkill
    {
        Task<IEnumerable<SkillModel>?> Search(int top, string skillname);
    }
}
