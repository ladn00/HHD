using HHD.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace HHD.ViewModels
{
    public class PostContentItemViewModel
    {
        public enum ContentItemTypeEnum { Text, Image, Title }

        public int? PostContentId { get; set; }

        public int ContentItemType { get; set; }

        public string? Value { get; set; }
    }

    public class PostViewModel
    {
        public int? PostId { get; set; }

        [Required(ErrorMessage = "Заголовок обязателен")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Описание обязателено")]
        public string? Intro { get; set; }

        public List<PostContentItemViewModel> ContentItems { get; set; } = new List<PostContentItemViewModel>();

        public PostStatusEnum Status { get; set; }
    }
}
