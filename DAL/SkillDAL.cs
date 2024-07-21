using HHD.DAL.Models;

namespace HHD.DAL
{
    public class SkillDAL : ISkillDAL
    {
        public async Task<int> Create(string skillname)
        {
            string sql = @"insert into Skill (SkillName)
                    values (@skillname) returning SkillId";

            return await DbHelper.QueryScalarAsync<int>(sql, new { skillname = skillname });
        }

        public async Task<IEnumerable<SkillModel>?> Search(int top, string skillname)
        {
            string sql = @"select SkillId, SkillName from Skill where SkillName like @skillname limit @top";
            return await DbHelper.QueryAsync<SkillModel>(sql, new { top = top, skillname = "%" + skillname + "%" });
        }

        public async Task<SkillModel> Get(string skillname)
        {
            string sql = @"select SkillId, SkillName from Skill where SkillName = @skillname limit 1";
            return await DbHelper.QueryScalarAsync<SkillModel>(sql, new { skillname = skillname });
        }

        public async Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId)
        {
            return await DbHelper.QueryAsync<ProfileSkillModel>(@$"
                        select ProfileSkillId, ProfileId, ps.SkillId, Level, SkillName
                        from ProfileSkill ps
							join Skill s on ps.SkillId = s.SkillId
                        where ProfileId = @profileId
                        ", new { profileId = profileId });
        }

        public async Task<int> AddProfileSkill(ProfileSkillModel model)
        {
            string sql = @"insert into ProfileSkill (ProfileId, SkillId, Level)
                    values (@ProfileId, @SkillId, @Level) returning ProfileSkillId";

            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }
    }
}
