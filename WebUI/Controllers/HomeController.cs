using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.EmailServices;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBookService _bookService;
        private UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private IEmailSender _emailSender;

        public HomeController(IBookService bookService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender)
        {
            _bookService = bookService;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserSignInModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if(user != null)
                {

                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                         ModelState.AddModelError("","Lütfen email hesabınıza gelen link ile üyeliğinizi onaylayın.");
                         return View("Index",model);
                    }

                    var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                    if (identityResult.Succeeded)
                    {
                        var roles =  await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new {area = "Admin"});
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new {area = "Member"});
                        }
                    }
                }
                ModelState.AddModelError("","Kullanıcı adı veya şifre hatalı");
            }

            return View("Index",model);
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
                 
                };
               var result = await _userManager.CreateAsync(user,model.Password);

               if (result.Succeeded)
               {
                  var addRoleResult = await _userManager.AddToRoleAsync(user, "Member");
                  if (addRoleResult.Succeeded)
                  {
                      // generate token 
                      var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                      var url = Url.Action("ConfirmEmail", "Home", new {userId = user.Id, token = code});

                      //email 
                      await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.",
                          $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:50499{url}'>tıklayınız.</a>");
                      return RedirectToAction("Index","Home");
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
        

        public async  Task<IActionResult> ConfirmEmail(string userId , string token)
        {
            if (userId == null || token == null)
            {
                TempData["message"] = "Geçersiz token.";
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["message"] = "Hesabınız onaylandı.";
                    return View();
                }
                
            }
            TempData["message"] = "Hesabınız onaylanmadı.";
            return View(); 
        }

    }
}