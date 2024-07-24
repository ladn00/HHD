namespace HHD.DAL.Models
{
    public class PostContentModel
    {
        public int? PostContentId { get; set; }
        public int? PostId { get; set; }
        public int ContentItemType { get; set; }
        public string Value { get; set; } = "";
    }
}
