using System.ComponentModel.DataAnnotations;

namespace HHD.DAL.Models
{
    public class UserModel
    {
        [Key]
        public int? UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public int Status { get; set; } = 0;
    }
}
