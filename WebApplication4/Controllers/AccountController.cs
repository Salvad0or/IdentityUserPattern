using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication4.Models;
using WebApplication4.Models.ViewModels;


namespace WebApplication4.Controllers
{
    /// <summary>
    /// Основной контроллект аутентификации
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Классы Identity для аутентификации
        /// </summary>
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Внедрение зависимостей важных сервисов Identity
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #region Регистрация

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationUserViewModel registrationUserViewModel)
        {

            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Email = registrationUserViewModel.Email,
                    UserName = registrationUserViewModel.Email,
                };

                ///Добавление в БД
                var result = await _userManager.CreateAsync(user, registrationUserViewModel.Password);

                ///Если всё ок прыгаем на главную
                if (result.Succeeded)
                {
                    ///Здесь добавляются необходимые клаймы
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Manager"));
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        #endregion

        #region Логин

        /// <summary>
        /// Метод гет для показа формы регистрации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Если всё прошло успешно аутентифицируем нового пользователя
        /// </summary>
        /// <param name="userLoginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {

                User user = await _userManager.FindByNameAsync(userLoginViewModel.Email);

                if (user is null)
                {
                    ModelState.AddModelError("", "Пользователь не найден");

                }


                var result = await _signInManager.PasswordSignInAsync(user, userLoginViewModel.Password, true,false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                

                ModelState.AddModelError("", "Неверный логин или пароль");
            }

            return View();
        }

        #endregion


        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Выход

        /// <summary>
        /// Всего одним взмахом руки вы покидаем наш логин.
        /// Метод логаут чистит аутентификационные куки
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}
