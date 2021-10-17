using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Extensions;
using Microsoft.Extensions.Localization;
using WebApplication1.Utils;
using Microsoft.AspNetCore.Localization;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> _logger;
        readonly IStringLocalizer _localizer;
        readonly AppUtils _utils;

        public HomeController(
            ILogger<HomeController> logger,
            IStringLocalizer localizer,
            AppUtils utils
            )
        {
            _logger = logger;
            _localizer = localizer;
            // var title = localizer["WelcomeTitle"];
            _utils = utils;
        }

        public IActionResult Index()
        {
            // ViewData
            ViewData["Message"] = "Hello";
            // ViewBag
            // ViewBag.Message = "Hello";
            //if (!HttpContext.Request.Cookies.ContainsKey("connectionId"))
            //{
            //    HttpContext.Response.Cookies.Append(
            //        "connectionId",
            //        Guid.NewGuid().ToString(),
            //        new Microsoft.AspNetCore.Http.CookieOptions
            //        {
            //            Expires = DateTimeOffset.Now.AddDays(1),
            //            HttpOnly = true,
            //        });
            //}
            if (HttpContext.Request.Query.ContainsKey("username"))
            {
                var username = HttpContext.Request.Query["username"].ToString();
                HttpContext.Session.Set("username", username);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string lang)
        {
            if (!_utils.AvailableLanguages.Any(l => l.Equals(lang)))
            {
                return BadRequest("Culture doesn`t exists");
            }
            // _utils.ChangeCulture(lang);
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)));
            return Json("Ok");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
