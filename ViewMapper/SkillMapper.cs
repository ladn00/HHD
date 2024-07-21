using System;
using HHD.DAL.Models;
using HHD.ViewModels;

namespace HHD.ViewMapper
{
    public class SkillMapper
    {
        public static ProfileSkillModel MapSkillViewModelToProfileSkillModel(SkillViewModel model)
        {
            return new ProfileSkillModel()
            {
                SkillName = model.Name,
                Level = model.Level
            };
        }
    }
}
