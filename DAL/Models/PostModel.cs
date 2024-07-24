namespace HHD.DAL.Models
{
    public class PostModel
    {
        public int? PostId { get; set; }

        public int UserId { get; set; }

        public string UniqueId { get; set; } = "";

        public string Title { get; set; } = "";

        public string Intro { get; set; } = "";

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public PostStatusEnum Status { get; set; }
    }
}
