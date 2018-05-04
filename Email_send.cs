using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Web;
namespace Bot_Application_mail
{
    public class Email_send
    {
        //private static void Main()
        //{
        //    Execute().Wait();
        //}
        public static async Task Execute()
        {
            var apikey = Environment.GetEnvironmentVariable("rams3098");
            var client = new SendGridClient("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");
            var from = new EmailAddress("ramyashree268@gmail.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("asbo15is@cmrit.ac.in", "Example User");
            var plainTextContent = "and easy to do anywhere ,even with c#";
            var htmlContent = "<strong>and easy to do anywhere ,even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            

        }
    }
}