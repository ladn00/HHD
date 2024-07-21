namespace HHD.DAL.Models
{
    public class ProfileSkillModel
    {
        public int ProfileSkillId { get; set; }
        public int ProfileId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public int Level { get; set; }
    }
}
