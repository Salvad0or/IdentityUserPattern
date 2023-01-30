using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Models
{
    /// <summary>
    /// Наш главный класс наследующий все классные вещи от IdentityUser.
    /// Здесь мы можем манипулировать как нам хочется
    /// </summary>
    public class User : IdentityUser
    {
        public string? Roles { get; set; }
    }
}
