using Consume.Models;
using Consume.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consume.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        /// <summary>
        /// Kullanıcının giriş yapmasını sağlar.
        /// </summary>
        /// <param name="model">Kullanıcı adı ve şifre içeren model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await _authService.LoginAsync(model.Username, model.Password);

                if (string.IsNullOrEmpty(token))
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
                    return View(model);
                }

               
                HttpContext.Session.SetString("Token", token);

                return RedirectToAction("Index", "Product");
            }

            return View(model);
        }

        /// <summary>
        /// Sessionda tokeni siler ve kullanıcının çıkış yapmasını sağlar.
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
           
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 401 durumu oluştuğunda Unauthorized sayfasına yönlendirir.
        /// </summary>
        /// <returns></returns>
        public IActionResult Unauthorized()
        {
            return View("Unauthorized");
        }

    }
}
