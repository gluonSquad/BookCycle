using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using WebUI.EmailServices;
using WebUI.Models;
using WebUI.TwoFactorServices;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBookService _bookService;
        private UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private IEmailSender _emailSender;
        private readonly TwoFactorService _twoFactorService;
        public HomeController(IBookService bookService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender, TwoFactorService twoFactorService)
        {
            _bookService = bookService;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _twoFactorService = twoFactorService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            foreach (var key in HttpContext.Request.Cookies.Keys)
            {
                HttpContext.Response.Cookies.Append(key, "", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
            }
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
                    if (_userManager.IsEmailConfirmedAsync(user).Result == false)
                    {
                        ModelState.AddModelError("", "Email adresiniz onaylanmamıştır.Lütfen epostanızı kontrol ediniz.");
                        return View("Index",model);
                    }

                    //TODO : Email gönderim işlemine bakılacak
                    //if (!await _userManager.IsEmailConfirmedAsync(user))
                    //{
                    //    ModelState.AddModelError("", "Lütfen email hesabınıza gelen link ile üyeliğinizi onaylayın.");
                    //    return View("Index", model);
                    //}

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

                        string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        string link = Url.Action("ConfirmEmail", "Home", new
                        {
                            userId = user.Id,
                            token = confirmationToken
                        },protocol:HttpContext.Request.Scheme);
                        Helper.EmailConfirmation.SendEmail(link, user.Email);

                        //TODO : Email gönderim işlemine bakılacak
                        //// generate token 
                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        //var callbackUrl = Url.Page("/Home/ConfirmEmail", pageHandler: null, values: new { area = "Identity", userId = user.Id, code = code }, protocol: Request.Scheme);


                        ////email 
                        //await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.",
                        //    $"Lütfen email hesabınızı onaylamak için linke <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>tıklayınız.</a>.");
                        return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword (PasswordResetViewModel passwordResetViewModel)
        {
            AppUser user = _userManager.FindByNameAsync(passwordResetViewModel.UserName).Result;
            if(user != null)
            {
                string passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;

                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Home", new
                {
                    userId = user.Id,
                    token = passwordResetToken
                }, HttpContext.Request.Scheme);

                Helper.PasswordReset.PasswordResetSendEmail(passwordResetLink,user.Email);

                ViewBag.Status = "successfull";
            }
            else
            {
                ModelState.AddModelError("", "Sistemde kayıtlı kullanıcı bulunamamıştır.");
            }
            return View(passwordResetViewModel);
        }


        public IActionResult ResetPasswordConfirm(string userId , string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm([Bind("PasswordNew")]PasswordResetViewModel passwordResetViewModel)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, passwordResetViewModel.PasswordNew);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    TempData["passwordResetInfo"] = "Şifreniz başarıyla yenilenmiştir.Yeni şifreniz ile giriş yapabilirsiniz.";
                    ViewBag.Status = "success";
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Hata meydana gelmiştir. Lütfen daha sonra tekrar deneyiniz.");
            }

            return View(passwordResetViewModel);
        }
    }
}