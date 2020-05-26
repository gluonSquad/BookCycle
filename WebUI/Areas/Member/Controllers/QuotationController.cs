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
using WebUI.Extensions;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class QuotationController : Controller
    {
        private IBookService _bookService;
        private readonly IBookAppUserService _bookAppUserService;
        private readonly UserManager<AppUser> _userManager;
        private IQuotationService _quotationService;
       

        public QuotationController(IBookService bookService, IBookAppUserService bookAppUserService, UserManager<AppUser> userManager, IQuotationService quotationService)
        {
            _bookService = bookService;
            _bookAppUserService = bookAppUserService;
            _userManager = userManager;
            _quotationService = quotationService;
        }

        public async Task<IActionResult> Index()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = appUser.Id;
            TempData["userId"] = userId;
            var model = new QuotationAddViewModel();
            model.Books = GetAppUserBookSelectListItems();
            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> Index(QuotationAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                var userId = appUser.Id;
                model.AppUserId = userId;
                Quotation quotation = new Quotation
                {
                    AppUserId = model.AppUserId,
                    QuotationText = model.QuotationText,
                    BookId = model.BookId,
                    CurrentPage = model.CurrentPage,
                    QuotesLike = model.QuotesLike,
                    CreatedOn = DateTime.Now,
                };
                _quotationService.Add(quotation);

            }

            return RedirectToAction("Index", "Quotation");
        }

        private List<SelectListItem> GetAppUserBookSelectListItems()
        {
            var result = new List<SelectListItem>();
            var userBooks = _bookAppUserService.GetByAppUserId(Convert.ToInt32((TempData["userId"])));
            foreach (var userBook in userBooks)
            {
                result.Add(new SelectListItem(userBook.Title, userBook.Id.ToString()));
            }
            return result;
        }


        public IActionResult List(string s, int page = 1)
        {
            TempData["Active"] = "quotation";
            ViewBag.CurrentPage = page;
            int totalPage;
            ViewBag.SearchedWord = s;
            var quotations = _quotationService.GetQuotations(out totalPage, s, page);
            ViewBag.TotalPage = totalPage;
            quotations.ShuffleMethod();
            return View(quotations);
        }
    }
}