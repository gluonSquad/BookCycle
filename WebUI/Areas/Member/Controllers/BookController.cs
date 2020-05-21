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
        private readonly UserManager<AppUser> _userManager;
        public BookController(IBookService bookService, IConfiguration configuration, ICategoryService categoryService, IAuthorService authorService, UserManager<AppUser> userManager)
        {
            _bookService = bookService;
            this.configuration = configuration;
            _categoryService = categoryService;
            _authorService = authorService;
            _userManager = userManager;
        }

        public IActionResult Index(string s , int page = 1 )
        {
            TempData["Active"] = "book";
            ViewBag.CurrentPage = page;
            int totalPage;

            ViewBag.SearchedWord = s;
            var books = _bookService.GetBookList(out totalPage, s , page);
            ViewBag.TotalPage = totalPage;
            ViewBag.Books = books;
           
            
            return View();
        }
    }
}