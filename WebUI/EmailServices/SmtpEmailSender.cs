using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebUI.EmailServices
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;

        public SmtpEmailSender(IOptions<EmailOptions> options)
        {
            _emailOptions = options.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(_emailOptions.SendGridApiKey, subject, htmlMessage, email);
        }

        private  Task Execute(string sendGridApiKey , string subject , string message , string email)
        {
            
            var client = new SendGridClient(sendGridApiKey);
            var from = new EmailAddress("sametirkoren@gmail.com", "Book Cycle");
            var to = new EmailAddress(email, "End User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "" , message);
            return  client.SendEmailAsync(msg);
        }
    }
}
