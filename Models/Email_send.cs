using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Web;
using RestaurantBot;
using ContosoScuba.Bot.CardProviders;

namespace Bot_Application_mail
{
    public class Email_send
    {
        //private static void Main()
        //{
        //    Execute().Wait();
        //}
        public static async Task Execute(BookingForm emailBooking, string text)
        {
            if (text == "confirmed booking" && emailBooking != null)
            {

                var apikey = Environment.GetEnvironmentVariable("rams3098");
                var client = new SendGridClient("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");
                var from = new EmailAddress("asbo15is@cmrit.ac.in", "Resturant");
                var subject = "Hi"+" "+emailBooking.Name+" , your order is confirmed";
                var to = new EmailAddress(emailBooking.Email_ID, "Resturant");
                var plainTextContent = "and easy to do anywhere ,even with c#";


                StringBuilder display = new StringBuilder();
                string table = "";
                //  display.Append("<table border = '1'>");
                if (FinalDisplay5.listUserResturantData.Count > 0)
                {
                    /* foreach (var item in FinalDisplay5.listUserResturantData)
                     {
                         display.Append("<tr>");
                         display.Append("<td>" + item.subDish + "</td>");
                         display.Append("<td>" + item.NumberOfPlates + "</td>");
                         display.Append("<td>" + item.TotalPrice + "</td>");
                         display.Append("</tr>");
                     }*/
                    foreach (var item in FinalDisplay5.listUserResturantData)
                    {

                        display.Append("<p>" +" "+ item.NumberOfPlates +" "+ "X" +" "+ item.subDish + "." + item.TotalPrice + ".00" + "</p>");
                    }
                    

                   table = display.ToString();
                }
                var style =@"table {
                    border - collapse: collapse;
                    width: 100 %;
                }

                th, td {
                    text - align: left;
                    padding: 8px;
                }

                tr: nth - child(even){ background - color: #f2f2f2}

            th {
                background - color: #4CAF50;color: white;
                }";

                /*  var htmlContent = $"<html><head><style>" + style + "</style></head><body><table style=\"width: 100%\" border=\"1\"><tr><th>First Name</th><th>Date</th><th>Time</th><th>Number of people</th><th>Phone number</th><th>Request</th><th>Email Id</th></tr>" +
                   $"<tr><td>" + emailBooking.Name + "</td><td>" + emailBooking.Date + "</td><td>" + emailBooking.Time + "</td><td>" + emailBooking.NumPeople + "</td><td>" + emailBooking.PhNum + "</td><td>" + emailBooking.Requests + "</td><td>" + emailBooking.Email_ID + "</td></tr>" +
                  $"</table>"+ "</br>" + "<p>Your meal preferences are recorded.</p>" + "</br>" + "<table style=\"width: 100%\" border=\"1\"><tr><th>Dish Name</th><th>Number of plates</th><th>Total Price</th></tr>" + table + "</table> " +
                   $"<h4>Thank you visit Again</h4></body></html>";*/
                var htmlContent = $"<html><head><style>" + style + "</style></head><body>"+"<p>Hi "+emailBooking.Name+" ,</P></br><p>Thanks, For Ordering.</p></br><p>Restaurant is preparing your Order.</p></br>"+
                                  $"<h4>Customer Information:</h4></br>"+emailBooking.Name+ "</br>"+"\n\n"+emailBooking.PhNum+"<h4>Order Summary:</h4></br>"+table +
                                  $"<h4>Thank you visit Again</h4></body></html>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                FinalDisplay5.listUserResturantData.Clear();
            }
            else
            {
                var apikey = Environment.GetEnvironmentVariable("rams3098");
                var client = new SendGridClient("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");
                var from = new EmailAddress("asbo15is@cmrit.ac.in", "Resturant");
                var subject = "Booking Cancelled Message";
                var to = new EmailAddress(emailBooking.Email_ID, "Resturant");
                var plainTextContent = "Your Booking has been cancelled";
                var htmlContent = "Your Booking has been cancelled,Thank You";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }


        }
    }
}