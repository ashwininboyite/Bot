using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Linq;
namespace SpeechToText
{
    public class Number
    {
        public void sendSms(string phonenumber , string name)
        {
            string message = HttpUtility.UrlEncode("This is your message");
            using (var wb = new WebClient())
            {
                var phnum = phonenumber;
                string Name = name;
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                    {"apikey","PN9+qi+K3Ew-cfBxHpSW10AGfcxENdTVcsVPCey5lz" },
                    {"numbers" ,phnum },
                    {"message" ,"Your Resturaunt Booking is confirmed " + "\n\n" + "Booking Name :"+ "\t\t" + Name },
                    {"sender","TXTLCL" }
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
            }
        }
    }
}
