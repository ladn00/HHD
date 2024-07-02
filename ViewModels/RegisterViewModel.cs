using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HHD.ViewModels
{
    public class RegisterViewModel 
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [RegularExpression("^(?=.*[A-Za-zА-Яа-я]*)(?=.*\\d*)(?=.*[*@#.$%&^]*).{4,}$", 
            ErrorMessage = "Длина пароля должна быть не менее 4-ех символов. Допустимые символы: А-я A-z *@#.$%&^")]
        public string? Password { get; set; }
    }
}
