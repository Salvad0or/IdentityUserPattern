using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage ="Введите E-mail")]
        [StringLength(25,MinimumLength =7,ErrorMessage = "Недопустимая длина логина")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите Пароль")]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Недопустимая длина пароля")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
    }
}
