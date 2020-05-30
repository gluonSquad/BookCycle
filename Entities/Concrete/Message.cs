using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Message
    {
        public Message()
        {
            When = DateTime.Now;
        }
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime When { get; set; }


        public int AppUserId { get; set; }

        public virtual AppUser Sender{ get; set; }
    }
}
