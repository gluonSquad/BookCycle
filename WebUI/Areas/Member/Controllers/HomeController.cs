using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebUI.Areas.Member.Models;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles="Member")]
    public class HomeController : Controller
    {
        private IBookService _bookService;
        private IConfiguration configuration;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
      
        public HomeController(IBookService bookService, IConfiguration configuration, ICategoryService categoryService, IAuthorService authorService)
        {
            _bookService = bookService;
            this.configuration = configuration;
            _categoryService = categoryService;
            _authorService = authorService;
         
        }
        public IActionResult Index()
        {
            TempData["Active"] = "home";
            return View(new BookAddViewModel());
        }



        [HttpPost]
        public IActionResult GetGoodReadUriAndAddBook(string title, string author)
        {
            string myKey = configuration.GetSection("goodreadsDeveloperKey").GetSection("key").Value;
            string geturi = "http://www.goodreads.com/book/title?format={0}&author={1}&key={2}&title={3}";
            var uri = String.Format(geturi, "xml", author, myKey, title);
            string responseData = String.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                using (Stream s = res.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(s))
                    {
                        responseData = sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return Json(null);
            }
            XDocument xmlResponsesData = XDocument.Parse(responseData);
            XElement goodreadsRoot = xmlResponsesData.Element("GoodreadsResponse");
            XElement bookRootElement = goodreadsRoot.Element("book");
            XElement titleElement = bookRootElement.Element("title");
            XElement isbnElement = bookRootElement.Element("isbn13");
            XElement descriptionElement = bookRootElement.Element("description");
            XElement numPagesElement = bookRootElement.Element("num_pages");
            XElement bookImgElement = bookRootElement.Element("image_url");
            XElement publisherElement = bookRootElement.Element("publisher");
            XElement publisherYear = bookRootElement.Element("publication_year");
            XElement publisherMonth = bookRootElement.Element("publication_month");
            XElement publisherDay = bookRootElement.Element("publication_day");
            string bookTitleValue = titleElement.Value;
            string bookIsbnValue = isbnElement.Value;
            string bookDescriptionValue = descriptionElement.Value;
            string bookNumPagesValue = numPagesElement.Value;
            string bookImgValue = bookImgElement.Value;
            string bookPublisherYearValue = publisherYear.Value;
            string bookPublisherMonthValue = publisherMonth.Value;
            string bookPublisherDayValue = publisherDay.Value;
            var bookImg = string.Empty;
            if (bookImgValue.Contains("_."))
            {
                var bookImgSplit = bookImgValue.Split("_.");
                var bookImgJpg = bookImgSplit[1];
                var bookImgUrl = bookImgSplit[0].Split("._")[0];
                bookImg = String.Join(".", bookImgUrl, bookImgJpg);
            }
            else
                bookImg = bookImgValue;

            var bookPublisher = publisherElement.Value;
            var bookPublisherDate = (bookPublisherMonthValue != "" && bookPublisherDayValue != "" ? String.Join("-", bookPublisherYearValue, bookPublisherMonthValue,
                bookPublisherMonthValue) : bookPublisherYearValue);
            var cleanDescription = Regex.Replace(bookDescriptionValue, "<[^>]*>", string.Empty);
            Category categoryEntity = new Category
            {
                Name = "Kitap"
            };
            var checkCategory = _categoryService.ExitsCategory(categoryEntity.Name);
            if (checkCategory == null)
            {
                _categoryService.Add(categoryEntity);
            }

            else
            {
                categoryEntity = checkCategory;
            }

            Author authorEntity = new Author
            {
                Name = author
            };
            var checkAuthor = _authorService.ExitsAuthor(authorEntity.Name);
            if (checkAuthor == null)
            {
                _authorService.Add(authorEntity);

            }
            else
            {
                authorEntity = checkAuthor;
            }

            Book entity = new Book
            {

                Title = bookTitleValue,
                Description = cleanDescription,
                BookPages = Convert.ToInt32(bookNumPagesValue),
                BookImageUrl = bookImg,
                Isbn = bookIsbnValue,
                AuthorId = authorEntity.Id,
                CategoryId = categoryEntity.Id,
                BookPublisher = bookPublisher,
                DatePublished = bookPublisherDate,
                Category = categoryEntity,
                Author = authorEntity
            };

            var checkBook = _bookService.CheckIsbnAndTitle(entity.Isbn, entity.Title);
            if (!checkBook)
            {
                return Json(checkBook);
            }
            else
            {
                _bookService.Add(entity);
                //var jsonBook = JsonConvert.SerializeObject(entity);
                return Json(checkBook);
            }




        }
    }
}