using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Member.Models
{
    public class CommentAddViewModel
    {
        public int Id { get; set; }

        public string HeadLine { get; set; }

        public string ReviewText { get; set; }

        public string BookStatus { get; set; }

        public int ReviewLike { get; set; }

        public int Rating { get; set; }


        public int BookId { get; set; }

        public int AppUserId { get; set; }
        public  List<SelectListItem> Books { get; set; }
    }
}
