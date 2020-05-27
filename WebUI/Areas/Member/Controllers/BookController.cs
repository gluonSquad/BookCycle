using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebUI.Areas.Member.Models;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]

    public class BookController : Controller
    {
       
        private IBookService _bookService;
        private IConfiguration configuration;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private readonly IBookAppUserService _bookAppUserService;
        private readonly UserManager<AppUser> _userManager;
        public BookController(IBookService bookService, IConfiguration configuration, ICategoryService categoryService, IAuthorService authorService, UserManager<AppUser> userManager, IBookAppUserService bookAppUserService)
        {
            _bookService = bookService;
            this.configuration = configuration;
            _categoryService = categoryService;
            _authorService = authorService;
            _userManager = userManager;
            _bookAppUserService = bookAppUserService;
        }

        public async Task<IActionResult> Index(string s , int page = 1 )
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["appUserId"] = appUser.Id; 
            TempData["Active"] = "book";
            ViewBag.CurrentPage = page;
            int totalPage;

            ViewBag.SearchedWord = s;
            var books = _bookService.GetBookList(out totalPage, s , page);
            ViewBag.TotalPage = totalPage;
            ViewBag.Books = books;
            return View();
        }

        [HttpPost]
        public IActionResult Index(int bookId, int appUserId)
        {
            var result = _bookAppUserService.AddBookAppUser(bookId,appUserId);
            if (result == 1)
            {
                return Json(true);
            }

            return Json(false);

        }




    }
}