using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssigningTasks.Sample.Helpers;
using AssigningTasks.Sample.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssigningTasks.Sample.Controllers
{
    public class AccountController : Controller
    {
        private readonly Dictionary<string, string> _AdminPass;

        public AccountController()
        {
            _AdminPass = new Dictionary<string, string>
            {
                { "admin", "ahmadtantowi" },
                { "pass", "akudankau" }
            };
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginVM)
        {
            ViewBag.ErrorMessage = null;

            if (loginVM.Username.Equals(_AdminPass["admin"])
                &&
                loginVM.Password.Equals(_AdminPass["pass"]))
            {
                AppCookieHelper.Set(_AdminPass, "admin", loginVM.IsRemember, "AssigningTasksSample", this.HttpContext);

                return RedirectPermanent(SiteHelper.GetBaseUrl(HttpContext.Request) + "/Assignee/Simulation");
            }

            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            AppCookieHelper.LogOut(this.HttpContext);

            return RedirectPermanent(SiteHelper.GetBaseUrl(HttpContext.Request) + "/Account/Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
