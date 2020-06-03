using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.TwoFactorServices
{
    public class TwoFactorOptions
    {
        public string SendGridApiKey { get; set; }
        public int CodeTimeExpire { get; set; }
    }
}
