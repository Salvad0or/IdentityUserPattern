using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Registration(RegistrationUserViewModel registrationUserViewModel)
        {

            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Email = registrationUserViewModel.Email,
                    UserName = registrationUserViewModel.Email,
                };

                ///Добавление в БД
                var result = _userManager.CreateAsync(user, registrationUserViewModel.Password);

                ///Если всё ок прыгаем на главную
                if (result.Result.Succeeded)
                {
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
        /// Если всё прошло успешно записываем нового пользователя
        /// </summary>
        /// <param name="userLoginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = _signInManager.PasswordSignInAsync
                    (userLoginViewModel.Email, userLoginViewModel.Password, true, false);

                if (result.Result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Неверный логин или пароль");
            }

            return View();
        }

        #endregion

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
