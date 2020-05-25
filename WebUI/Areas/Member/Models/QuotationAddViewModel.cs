using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Member.Models
{
    public class QuotationAddViewModel
    {
        public int Id { get; set; }

        public string QuotationText { get; set; }

        public int CurrentPage { get; set; }

        public int QuotesLike { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;


        public int? BookId { get; set; }

        public int? AppUserId { get; set; }
        public List<SelectListItem> Books { get; set; }
    }
}
