using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models.ViewModels
{
    /// <summary>
    /// Класс для реигастриции View
    /// </summary>
    public class RegistrationUserViewModel
    {
        [Required(ErrorMessage ="Введите логин")]
        [StringLength(20, MinimumLength = 5,ErrorMessage ="Неверный размер e-mail")]
        [Display(Name ="E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите Пароль")]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Недопустимая длина пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
