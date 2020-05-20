using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "profile";
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserListViewModel model = new AppUserListViewModel();
            model.Id = appUser.Id;
            model.FirstName = appUser.FirstName;
            model.LastName = appUser.LastName;
            model.Email = appUser.Email;
            model.UserName = appUser.UserName;
            model.Description = appUser.Description;
            model.Picture = appUser.ProfileImageFile;
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Index(AppUserListViewModel model, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = _userManager.Users.FirstOrDefault(I => I.Id == model.Id);


                if (photo != null)
                {
                    string uzanti = Path.GetExtension(photo.FileName);
                    string photoName = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + photoName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    updatedUser.ProfileImageFile = photoName;
                }

                updatedUser.UserName = model.UserName;
                updatedUser.FirstName = model.FirstName;
                updatedUser.LastName = model.LastName;
                updatedUser.Email = model.Email;
                updatedUser.Description = model.Description;
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işleminiz başarı ile gerçekleşti.";
                    return RedirectToAction("Index");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }
    }
}