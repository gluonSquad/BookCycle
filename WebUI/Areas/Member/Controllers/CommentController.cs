using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Areas.Member.Models;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CommentController : Controller
    {
        private IBookService _bookService;
        private readonly IBookAppUserService _bookAppUserService;
        private readonly UserManager<AppUser> _userManager;
        private ICommentService _commentService;

        public CommentController(IBookAppUserService bookAppUserService, UserManager<AppUser> userManager, IBookService bookService, ICommentService commentService)
        {
            _bookAppUserService = bookAppUserService;
            _userManager = userManager;
            _bookService = bookService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = appUser.Id;
            TempData["userId"] = userId;
            var model = new CommentAddViewModel();
            model.Books = GetAppUserBookSelectListItems();
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Index(CommentAddViewModel model)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = appUser.Id;
            model.AppUserId = userId;
            Review comment = new Review
            {
                AppUserId = model.AppUserId,
                BookStatus = model.BookStatus,
                BookId = model.BookId,
                HeadLine = model.HeadLine,
                ReviewText = model.ReviewText,
                Rating = model.Rating,
                CreatedOn = DateTime.Now,
                ReviewLike = model.ReviewLike,

            };
            _commentService.Add(comment);
            return RedirectToAction("Index","Comment");
        }

        private List<SelectListItem> GetAppUserBookSelectListItems()
        {
            var result = new List<SelectListItem>();
            var userBooks  = _bookAppUserService.GetByAppUserId(Convert.ToInt32((TempData["userId"])));
            foreach (var userBook in userBooks)
            {
                result.Add(new SelectListItem(userBook.Title , userBook.Id.ToString()));
            }
            return result;
        }

       
    }
}