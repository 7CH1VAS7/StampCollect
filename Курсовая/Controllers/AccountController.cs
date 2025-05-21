using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Курсовая.Models;

namespace Курсовая.Controllers
{
    public class AccountController : Controller
    {
        private const string _adminLogin = "admin";
        private const string _adminPassword = "admin123"; // В реальном проекте храните хеш пароля!

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            // Проверка логина/пароля (в реальном проекте - проверка в БД)
            if (model.Login == _adminLogin && model.Password == _adminPassword)
            {
                // Создаем claims (утверждения) для пользователя
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Login),
                new Claim(ClaimTypes.Role, "Admin") // Добавляем роль
            };

                // Создаем объект ClaimsIdentity
                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                // Аутентифицируем пользователя
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true, // "Запомнить меня"
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                    });

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверный логин или пароль");
            return View("Index", model);
        }

        [Authorize] // Только для авторизованных
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
