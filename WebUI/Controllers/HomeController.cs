using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBookService _bookService;
        private UserManager<AppUser> _userManager;

        public HomeController(IBookService bookService, UserManager<AppUser> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]

        public async Task <IActionResult> Register(AppUserAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Description = model.Description
                };
               var result = await _userManager.CreateAsync(user,model.Password);

               if (result.Succeeded)
               {
                  var addRoleResult = await _userManager.AddToRoleAsync(user, "Member");
                  if (addRoleResult.Succeeded)
                  {
                      return RedirectToAction("Login");
                  }
                    foreach (var item in addRoleResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
               }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        } 

    }
}