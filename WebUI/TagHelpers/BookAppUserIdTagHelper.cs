using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelpers
{
    [HtmlTargetElement("getBookAppUserId")]
    public class BookAppUserIdTagHelper : TagHelper
    {
        private readonly IBookAppUserService _bookAppUserService;

        public BookAppUserIdTagHelper(IBookAppUserService bookAppUserService)
        {
            _bookAppUserService = bookAppUserService;
        }

        public int AppUserId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<Book> userbooks = _bookAppUserService.GetByAppUserId(AppUserId);
            int bookCount = userbooks.Count();

            string htmlString = $"{bookCount} <span>Kitap Sayısı</span>";
            output.Content.SetHtmlContent(htmlString);
        }
    }
}
