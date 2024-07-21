using HHD.DAL.Models;
using HHD.DAL;

namespace HHD.BL.Profile
{
    public class Skill : ISkill
    {
        private readonly ISkillDAL skillDAL;

        public Skill(ISkillDAL skillDAL)
        {
            this.skillDAL = skillDAL;
        }

        public async Task<IEnumerable<SkillModel>?> Search(int top, string skillname)
        {
            return await this.skillDAL.Search(top, skillname);
        }
    }
}
