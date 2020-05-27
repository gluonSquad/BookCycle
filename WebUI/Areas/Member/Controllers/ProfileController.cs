using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Areas.Admin.Models;
using WebUI.Areas.Member.Models;
using WebUI.Extensions;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles="Member")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IBookService _bookService;
        private readonly IBookAppUserService _bookAppUserService;

        public ProfileController(UserManager<AppUser> userManager, IBookService bookService, IBookAppUserService bookAppUserService)
        {
            _userManager = userManager;
            _bookService = bookService;
            _bookAppUserService = bookAppUserService;
        }

        public  async Task<IActionResult> Index(string user)
        {
            var books = _bookService.GetList();
            var userId = 0;
            AppUser currentUser = new AppUser();
            Models.Member member = new Models.Member();
            CustomModel model = new CustomModel();
            AppUser profileUser = new AppUser();
            TempData["Active"] = "profile";
            TempData["sa"] =TempData["deneme"];
            var userprofile = TempData["currentUser"];
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
           
            if(userprofile == null)
            {
                profileUser = null;

                member.Id = appUser.Id;
                member.FirstName = appUser.FirstName;
                member.LastName = appUser.LastName;
                member.Email = appUser.Email;
                member.UserName = appUser.UserName;
                member.Description = appUser.Description;
                member.Picture = appUser.ProfileImageFile;
                model.Member = member;
                userId = appUser.Id;
            }
            else
            {
                profileUser = await _userManager.FindByNameAsync(userprofile.ToString());
                member.Id = profileUser.Id;
                member.FirstName = profileUser.FirstName;
                member.LastName = profileUser.LastName;
                member.Email = profileUser.Email;
                member.UserName = profileUser.UserName;
                member.Description = profileUser.Description;
                member.Picture = profileUser.ProfileImageFile;
                model.Member = member;
                userId = profileUser.Id;
            }

            if(user == null)
            {
                currentUser = null;
                member.Id = appUser.Id;
                member.FirstName = appUser.FirstName;
                member.LastName = appUser.LastName;
                member.Email = appUser.Email;
                member.UserName = appUser.UserName;
                member.Description = appUser.Description;
                member.Picture = appUser.ProfileImageFile;
                model.Member = member;
                userId = appUser.Id;
            }

            else
            {
                currentUser = await _userManager.FindByNameAsync(user);

                member.Id = currentUser.Id;
                member.FirstName = currentUser.FirstName;
                member.LastName = currentUser.LastName;
                member.Email = currentUser.Email;
                member.UserName = currentUser.UserName;
                member.Description = currentUser.Description;
                member.Picture = currentUser.ProfileImageFile;
                model.Member = member;
                userId = currentUser.Id;
            }
        
         

            if(currentUser != null)
            {
                if (appUser.UserName != currentUser.UserName)
                {
                    model.IsProfile = false;
                }
                else
                {
                    model.IsProfile = true;
                }
            }

            if (profileUser != null)
            {
                if (appUser.UserName == profileUser.UserName)
                {
                    model.IsProfile = true;
                }
                else
                {
                    model.IsProfile = false;
                }
            }



            List<Review> reviews = new List<Review>();
            List<Quotation> quotations = new List<Quotation>();
           
            var userbooks = _bookAppUserService.GetByAppUserId(userId);
            foreach (var book in userbooks)
            {
                if (book.Reviews.Count > 0)
                {
                    reviews.AddRange(book.Reviews);
                }

                if (book.Quotations.Count > 0)
                {
                    quotations.AddRange(book.Quotations);
                }
            }

            quotations.ShuffleMethod();
            reviews.ShuffleMethod();
            ViewBag.Comment= reviews;
            ViewBag.Quotations= quotations;

            
          
            return View(model);
        }

        public async Task<IActionResult> Comment(string user)
        {
            var books = _bookService.GetList();
            var userId = 0;
            AppUser currentUser = new AppUser();
            Models.Member member = new Models.Member();
            CustomModel model = new CustomModel();
            AppUser profileUser = new AppUser();
            TempData["Active"] = "profile";
            TempData["sa"] = TempData["deneme"];
            var userprofile = TempData["currentUser"];
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (userprofile == null)
            {
                profileUser = null;

                member.Id = appUser.Id;
                member.FirstName = appUser.FirstName;
                member.LastName = appUser.LastName;
                member.Email = appUser.Email;
                member.UserName = appUser.UserName;
                member.Description = appUser.Description;
                member.Picture = appUser.ProfileImageFile;
                model.Member = member;
                userId = appUser.Id;
            }
            else
            {
                profileUser = await _userManager.FindByNameAsync(userprofile.ToString());
                member.Id = profileUser.Id;
                member.FirstName = profileUser.FirstName;
                member.LastName = profileUser.LastName;
                member.Email = profileUser.Email;
                member.UserName = profileUser.UserName;
                member.Description = profileUser.Description;
                member.Picture = profileUser.ProfileImageFile;
                model.Member = member;
                userId = profileUser.Id;
            }

            if (user == null)
            {
                currentUser = null;
                member.Id = appUser.Id;
                member.FirstName = appUser.FirstName;
                member.LastName = appUser.LastName;
                member.Email = appUser.Email;
                member.UserName = appUser.UserName;
                member.Description = appUser.Description;
                member.Picture = appUser.ProfileImageFile;
                model.Member = member;
                userId = appUser.Id;
            }

            else
            {
                currentUser = await _userManager.FindByNameAsync(user);

                member.Id = currentUser.Id;
                member.FirstName = currentUser.FirstName;
                member.LastName = currentUser.LastName;
                member.Email = currentUser.Email;
                member.UserName = currentUser.UserName;
                member.Description = currentUser.Description;
                member.Picture = currentUser.ProfileImageFile;
                model.Member = member;
                userId = currentUser.Id;
            }



            if (currentUser != null)
            {
                if (appUser.UserName != currentUser.UserName)
                {
                    model.IsProfile = false;
                }
                else
                {
                    model.IsProfile = true;
                }
            }

            if (profileUser != null)
            {
                if (appUser.UserName == profileUser.UserName)
                {
                    model.IsProfile = true;
                }
                else
                {
                    model.IsProfile = false;
                }
            }



            List<Review> reviews = new List<Review>();
            List<Quotation> quotations = new List<Quotation>();

            var userbooks = _bookAppUserService.GetByAppUserId(userId);
            foreach (var book in userbooks)
            {
                if (book.Reviews.Count > 0)
                {
                    reviews.AddRange(book.Reviews);
                }

                if (book.Quotations.Count > 0)
                {
                    quotations.AddRange(book.Quotations);
                }
            }

            quotations.ShuffleMethod();
            reviews.ShuffleMethod();
            ViewBag.Comment = reviews;
            ViewBag.Quotations = quotations;



            return View(model);
        }

        public async Task<IActionResult> Quotation(string user)
        {
            var books = _bookService.GetList();
            var userId = 0;
            AppUser currentUser = new AppUser();
            Models.Member member = new Models.Member();
            CustomModel model = new CustomModel();
            AppUser profileUser = new AppUser();
            TempData["Active"] = "profile";
            TempData["sa"] = TempData["deneme"];
            var userprofile = TempData["currentUser"];
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (userprofile == null)
            {
                profileUser = null;

                member.Id = appUser.Id;
                member.FirstName = appUser.FirstName;
                member.LastName = appUser.LastName;
                member.Email = appUser.Email;
                member.UserName = appUser.UserName;
                member.Description = appUser.Description;
                member.Picture = appUser.ProfileImageFile;
                model.Member = member;
                userId = appUser.Id;
            }
            else
            {
                profileUser = await _userManager.FindByNameAsync(userprofile.ToString());
                member.Id = profileUser.Id;
                member.FirstName = profileUser.FirstName;
                member.LastName = profileUser.LastName;
                member.Email = profileUser.Email;
                member.UserName = profileUser.UserName;
                member.Description = profileUser.Description;
                member.Picture = profileUser.ProfileImageFile;
                model.Member = member;
                userId = profileUser.Id;
            }

            if (user == null)
            {
                currentUser = null;
                member.Id = appUser.Id;
                member.FirstName = appUser.FirstName;
                member.LastName = appUser.LastName;
                member.Email = appUser.Email;
                member.UserName = appUser.UserName;
                member.Description = appUser.Description;
                member.Picture = appUser.ProfileImageFile;
                model.Member = member;
                userId = appUser.Id;
            }

            else
            {
                currentUser = await _userManager.FindByNameAsync(user);

                member.Id = currentUser.Id;
                member.FirstName = currentUser.FirstName;
                member.LastName = currentUser.LastName;
                member.Email = currentUser.Email;
                member.UserName = currentUser.UserName;
                member.Description = currentUser.Description;
                member.Picture = currentUser.ProfileImageFile;
                model.Member = member;
                userId = currentUser.Id;
            }



            if (currentUser != null)
            {
                if (appUser.UserName != currentUser.UserName)
                {
                    model.IsProfile = false;
                }
                else
                {
                    model.IsProfile = true;
                }
            }

            if (profileUser != null)
            {
                if (appUser.UserName == profileUser.UserName)
                {
                    model.IsProfile = true;
                }
                else
                {
                    model.IsProfile = false;
                }
            }



            List<Review> reviews = new List<Review>();
            List<Quotation> quotations = new List<Quotation>();

            var userbooks = _bookAppUserService.GetByAppUserId(userId);
            foreach (var book in userbooks)
            {
                if (book.Reviews.Count > 0)
                {
                    reviews.AddRange(book.Reviews);
                }

                if (book.Quotations.Count > 0)
                {
                    quotations.AddRange(book.Quotations);
                }
            }

            quotations.ShuffleMethod();
            reviews.ShuffleMethod();
            ViewBag.Comment = reviews;
            ViewBag.Quotations = quotations;



            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CustomModel model)
        {
            TempData["currentUser"] = model.Member.UserName;
            if (ModelState.IsValid)
            {

                var updatedUser = _userManager.Users.FirstOrDefault(I => I.Id == model.Member.Id);


                if (model.MyFile != null)
                {
                    string uzanti = Path.GetExtension(model.MyFile.FileName);
                    string photoName = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + photoName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.MyFile.CopyToAsync(stream);
                    }

                    updatedUser.ProfileImageFile = photoName;
                }

                updatedUser.Id = model.Member.Id;
                updatedUser.UserName = model.Member.UserName;
                updatedUser.FirstName = model.Member.FirstName;
                updatedUser.LastName = model.Member.LastName;
                updatedUser.Email = model.Member.Email;
                updatedUser.Description = model.Member.Description;
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    TempData["username"] = updatedUser.UserName;
                    TempData["deneme"] = "Güncelleme işleminiz başarı ile gerçekleşti.";
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> Comment(CustomModel model)
        {
            TempData["currentUser"] = model.Member.UserName;
            if (ModelState.IsValid)
            {

                var updatedUser = _userManager.Users.FirstOrDefault(I => I.Id == model.Member.Id);


                if (model.MyFile != null)
                {
                    string uzanti = Path.GetExtension(model.MyFile.FileName);
                    string photoName = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + photoName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.MyFile.CopyToAsync(stream);
                    }

                    updatedUser.ProfileImageFile = photoName;
                }

                updatedUser.Id = model.Member.Id;
                updatedUser.UserName = model.Member.UserName;
                updatedUser.FirstName = model.Member.FirstName;
                updatedUser.LastName = model.Member.LastName;
                updatedUser.Email = model.Member.Email;
                updatedUser.Description = model.Member.Description;
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    TempData["username"] = updatedUser.UserName;
                    TempData["deneme"] = "Güncelleme işleminiz başarı ile gerçekleşti.";
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> Quotation(CustomModel model)
        {
            TempData["currentUser"] = model.Member.UserName;
            if (ModelState.IsValid)
            {

                var updatedUser = _userManager.Users.FirstOrDefault(I => I.Id == model.Member.Id);


                if (model.MyFile != null)
                {
                    string uzanti = Path.GetExtension(model.MyFile.FileName);
                    string photoName = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + photoName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.MyFile.CopyToAsync(stream);
                    }

                    updatedUser.ProfileImageFile = photoName;
                }

                updatedUser.Id = model.Member.Id;
                updatedUser.UserName = model.Member.UserName;
                updatedUser.FirstName = model.Member.FirstName;
                updatedUser.LastName = model.Member.LastName;
                updatedUser.Email = model.Member.Email;
                updatedUser.Description = model.Member.Description;
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    TempData["username"] = updatedUser.UserName;
                    TempData["deneme"] = "Güncelleme işleminiz başarı ile gerçekleşti.";
                    return RedirectToAction("Index");
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