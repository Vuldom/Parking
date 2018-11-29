using System;
using Educo.Parking.Business.Services.EmailService;
using Educo.Parking.Business.Managers;
using Educo.Parking.Data;
using Educo.Parking.Shell.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Educo.Parking.Shell.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AccountManager accountManager;
        private readonly IdentityAccountManager identityAccountManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(AccountManager accountManager, IdentityAccountManager identityAccountManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.accountManager = accountManager;
            this.identityAccountManager = identityAccountManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                string password = await accountManager.PasswordRecovery(forgotPasswordModel.Username);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Очистить cookie, перед входом в систему
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await identityAccountManager.LoginAsync(model.Login, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    TempData["OK"] = "Вы успешно вошли в систему.";
                    return RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(SignOut));
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Ошибка входа в систему.");
                    return View(model);
                }
            }

            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await identityAccountManager.LogoutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Запросить перенаправление на внешний логин.
            string redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            AuthenticationProperties properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            ExternalLoginInfo externalLoginInfo = await signInManager.GetExternalLoginInfoAsync();

            if (externalLoginInfo == null)
            {
                return RedirectToAction(nameof(Login));
            }
            // Войти в систему с помощью этого внешнего поставщика входа, если у пользователя уже есть логин.
            Microsoft.AspNetCore.Identity.SignInResult signInResult = signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, true).Result;
            if (signInResult.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                //Если у пользователя нет учетной записи, надо создать учетную запись.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = externalLoginInfo.LoginProvider;
                string email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

                return View("ExternalLoginConfirmation", new ExternalLoginViewModel { Email = email, returnUrl = returnUrl });
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    throw new ApplicationException("Ошибка при загрузке внешней информации входа во время подтверждения.");
                }

                ApplicationUser userEntity = new ApplicationUser { UserName = info.Principal.FindFirstValue(ClaimTypes.Email), Email = model.Email };
                IdentityResult result = await userManager.CreateAsync(userEntity);

                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(userEntity, info);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(userEntity, isPersistent: false);
                        TempData["OK"] = "Вы успешно вошли в систему.";
                        return RedirectToLocal(model.returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Ошибка входа в систему.");
                }
            }

            return View(nameof(ExternalLogin), model);
        }
    }
}
