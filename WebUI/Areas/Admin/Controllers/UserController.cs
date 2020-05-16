using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class UserController : Controller
    {
        private IAppUserService _appUserService;

        public UserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public IActionResult Index(string s , int page=1)
        {
            TempData["Active"] = "user";
            ViewBag.CurrentPage = page;
            //ViewBag.TotalPage = (int)Math.Ceiling((double)_appUserService.GetNotAdmin().Count() / 3);
            int totalPage;

            ViewBag.SearchedWord = s;

            var users = _appUserService.GetNotAdmin(out totalPage , s, page);
            ViewBag.TotalPage = totalPage;
            ViewBag.Users = users;
            return View();
        }
    }
}