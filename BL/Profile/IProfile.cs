using HHD.DAL.Models;

namespace HHD.BL.Profile
{
    public interface IProfile
    {
        Task<IEnumerable<ProfileModel>> Get(int userId);

        Task Update(ProfileModel profileModel);

        Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId);

        Task AddProfileSkill(ProfileSkillModel model);
    }
}
