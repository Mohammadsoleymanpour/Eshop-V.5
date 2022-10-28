using Application.Interface;
using Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;
using Application.Convertor;
using Application.Sender;
using Domain.Models.Enums;
using Domain.ViewModels.User;
using Eshop.Controllers.Shared;
using Application.Security;
using Domain.Models.UserAgg;

namespace Eshop.Controllers
{
    public class AccountController : BaseSiteController
    {
        private IUserService _userService;
        private IViewRenderService _viewRenderService;
        private ILoggerService _loggerService;

        public AccountController(IUserService userService, IViewRenderService viewRenderService, ILoggerService loggerService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
            _loggerService = loggerService;
        }



        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Email", "لطفا اطلاعات را صحیح وارد کنید");
                return View(user);
            }


            if (_userService.UserIsExist(user))
            {
                ViewBag.IsExist = true;
                return View(user);
            }


            User GetUser = _userService.RegisterUser(user);

            string body = _viewRenderService.RenderToStringAsync("_activeEmail", GetUser);
            SendEmail.Send(GetUser.Email, "فعالسازی حساب", body);



            return View("SuccessRegister");
        }

        #endregion

        #region Login
        [Route("/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Profile");

            return View();
        }

        [Route("/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userService.LoginUser(login);
            if (user == null || user.Status == Status.NotActive)
            {
                ViewBag.Error = true;
                ModelState.AddModelError("Email", "کابری با مشخصات وارد شده یافت نشد .");
                return View(login);
            }

            var cliam = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Email)
                    };

            var identity = new ClaimsIdentity(cliam, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {

                IsPersistent = login.RememberMe
            };

            await HttpContext.SignInAsync(principal, properties);
            await _loggerService.AddLog(user.Id, user.Id, "ورود به حساب کاربری", LogType.UserLogin);
            ViewBag.IsSuccess = true;
            return View();
        }


        #endregion

        #region Logout
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _loggerService.AddLog(User.GetUserId(), User.GetUserId(), "ورود به حساب کاربری", LogType.UserLogin);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion

        #region ActiveAccount

        public IActionResult ActiveAccount(string id)
        {

            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region ForgotPassword
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }


            string fixeEmail = FixedText.FixEmail(model.Email);
            var user = _userService.GetUserByEmail(fixeEmail);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد");
                return View(model);
            }

            string bodyEmail = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);

            SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion

        #region ResetPassword
        [Route("ResetPassword/{id}")]
        public async Task<IActionResult> ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel
            {
                ActiveCode = id,
            });
        }

        [Route("ResetPassword/{id}")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }

            var user = _userService.GetUserByActiveCode(model.ActiveCode);
            if (user==null)
            {
                ModelState.AddModelError("Password","کاربری با مشخصات وارد شده یافت نشد");
            }
            var res = _userService.ResetPassword(model.Password, user.Id);
            if (res!=true)
            {
                return BadRequest();
            }
            return Redirect("/Login");
        }
        #endregion
    }
}
