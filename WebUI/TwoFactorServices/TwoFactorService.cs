using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.TwoFactorServices
{
    public class TwoFactorService
    {
        private TwoFactorOptions _twoFactorOptions;
        public TwoFactorService(IOptions<TwoFactorOptions> options)
        {
            _twoFactorOptions = options.Value;
        }
        public int GetCodeVerification()
        {
            Random rnd = new Random();
            return rnd.Next(1000,9999);

        }
        public int  TimeLeft(HttpContext context)
        {
            if(context.Session.GetString("currentTime") == null)
            {
                context.Session.SetString("currentTime", DateTime.Now.AddSeconds(_twoFactorOptions.CodeTimeExpire).ToString());
            }

            DateTime currentTime = DateTime.Parse(context.Session.GetString("currentTime").ToString());

            int timeLeft = (int)(currentTime - DateTime.Now).TotalSeconds;

            if(timeLeft <= 0)
            {
                context.Session.Remove("currentTime");
                return 0;
            }
            else
            {
                return timeLeft;
            }
        }
    }
}
