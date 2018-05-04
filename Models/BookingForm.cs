using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace RestaurantBot
{
    [Serializable]
    public class BookingForm
    {
        [Prompt("What date would you like to make a booking for? Please enter in M/dd/yyyy.")]
        public DateTime Date { get; set; }
        // Don't forget to make datetime nullable if it's optional!
        [Prompt("What is your preferred time? Please use HH:mm format e.g. 08.0am or 17:00pm")]
        //[Optional]
        public DateTime Time { get; set; }
        [Prompt("Your name?")]
        public string Name { get; set; }
        [Prompt("How many people will there be?")]
        public int NumPeople { get; set; }
        [Prompt("Can I get your phone number please?")]
        public string PhNum { get; set; }
        [Prompt("Can I get your Email_Id")]
        public string Email_ID { get; set; }
        [Prompt("Enter any additional requests or notes. Say None if you don't have any.")]
        [Optional]
        public string Requests { get; set; }
        public static IForm<BookingForm> BuildForm()
        {

            return new FormBuilder<BookingForm>()
                //.Field(nameof(Date,  validate : ValidateDate))
                .Field(nameof(Date), validate: ValidateDate)
                .Field(nameof(Time), active: IsTimeAdded, validate: ValidateTime)
                .AddRemainingFields()
                .Field(nameof(PhNum), validate: ValidatePhNum)
                .Field(nameof(Email_ID), validate: ValidateEmailID)


                .Confirm("Confirm booking on {Date:d} at {Time:t}? (Y/N)")
                .Build();
        }

        public static Task<ValidateResult> ValidateTime(BookingForm state, object response)

        {
            var result = new ValidateResult();
            //user entered date and default time
            DateTime var = state.Date;
            //user entered time and default date
            DateTime user_time = (DateTime)response;
            //system date and time in DateTime format
            DateTime system_dt = System.DateTime.Now;
            //only user entered date
            var Entered_date = var.ToShortDateString();
            //only user entered time
            var Entered_time = user_time.ToShortTimeString();
            //user entered date and user entered time in string formate
            string myDateTime = Entered_date + " " + Entered_time;
            //System date and time in string format
            string systemDateTime = system_dt.ToString();
            //user entered date and time in dateTime format
            DateTime finalDate = Convert.ToDateTime(myDateTime);
         
            //Compare the user time always greater or  equal to system time.
            var result_comp = finalDate.CompareTo(system_dt);
            if (result_comp < 1 || system_dt.ToString("HH:mm") == "20:30")
            {
                result.IsValid = false;
                result.Feedback = "Sorry, that time is not available,Enter proper time!!!!";
            }
            else if (system_dt.ToString("HH:mm") == "20:30")
            {
                // If time not available
                result.IsValid = false;
                result.Feedback = "Sorry, that time is not available,Enter proper time!!!!";
            }
            else
            {
                //day for coressponding date 
                //DateTime finalDate1= DateTime.ParseExact(myDateTime, "MM/dd/yy h:mm:ss tt", CultureInfo.InvariantCulture);
                string toDay = finalDate.ToString("dddd");
                if (toDay == "Monday" || toDay == "Tuesday" || toDay == "Wednesday" || toDay == "Thursday" || toDay == "Friday")
                {
                    //"Monday to Friday: 8.00am to 17.00pm(5.pm)"
                    TimeSpan lowertimeSpan = new TimeSpan(8, 00, 00);
                    TimeSpan UppertimeSpan = new TimeSpan(20, 00, 00);
                    TimeSpan timeSpan = user_time.TimeOfDay;
                    bool value;
                    if (lowertimeSpan == timeSpan || UppertimeSpan == timeSpan)
                    {
                        //  if value = true than I can Book;
                        value = true;
                        result.IsValid = true;
                        result.Value = response;
                    }
                    else if (lowertimeSpan < timeSpan && UppertimeSpan > timeSpan)
                    {
                        //  if value = true than I can Book;
                        value = true;
                        result.IsValid = true;
                        result.Value = response;
                    }
                    else
                    {
                        //else value = false than I can not Book;
                        value = false;
                        result.IsValid = false;
                        result.Value = response;
                    }
                }
                else if(toDay == "Saturday" || toDay== "Sunday")
                {
                    //"Saturday and sunday: 8.00am to 13.00pm(1.pm)
                    TimeSpan lowertimeSpan = new TimeSpan(8, 00, 00);
                    TimeSpan UppertimeSpan = new TimeSpan(13, 00, 00);
                    TimeSpan timeSpan = user_time.TimeOfDay;
                    bool value;
                    if(lowertimeSpan == timeSpan || UppertimeSpan == timeSpan)
                    {
                        //  if value = true than I can Book;
                        value = true;
                        result.IsValid = true;
                        result.Value = response;
                    }
                    else if (lowertimeSpan < timeSpan && UppertimeSpan > timeSpan)
                    {
                        //  if value = true than I can Book;
                        value = true;
                        result.IsValid = true;
                        result.Value = response;
                    }
                    else
                    {
                        //else value = false than I can not Book;
                        value = false;
                        result.IsValid = false;
                        result.Value = response;
                    }
                }
                
            }
            return Task.FromResult(result);
        }
        private static bool IsTimeAdded(BookingForm state)
        {
            if (state.Date.TimeOfDay.TotalSeconds == 0)
            {
                return true;
            }
            return false;
        }
        private static Task<ValidateResult> ValidatePhNum(BookingForm state, object response)
        {
            var result = new ValidateResult();
            string phoneNumber = response.ToString();
            if (phoneNumber.Length == 10)
            {
                if (IsPhNum((string)response))
                {
                    result.IsValid = true;
                    result.Value = response;
                }

                else
                {
                    result.IsValid = false;
                    result.Feedback = "Make sure the phone number you entered are all numbers.";
                }
            }
            else
            {
                result.IsValid = false;
                result.Feedback = "Phone number is  invalid please enter the valid phone number";
            }
            return Task.FromResult(result);

        }
        private static bool IsPhNum(string response)
        {
            if (Regex.IsMatch(response, @"^\d+$"))
            {
                return true;
            }
            return false;
        }

        public static Task<ValidateResult> ValidateEmailID(BookingForm state, object response)
        {
            var result = new ValidateResult();
            var emailid = response.ToString();
            // validation of email id 
            // Hard coded for demo purposes
            if (!Regex.IsMatch(emailid, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                // If time not available
                result.IsValid = false;
                result.Feedback = "Enter the valid Email";
            }
            else
            {
                result.IsValid = true;
                result.Value = response;
            }
            return Task.FromResult(result);
        }
        private static Task<ValidateResult> ValidateDate(BookingForm state, object response)
        {
            var result = new ValidateResult();
            //user enterd date in dateTime formate
            DateTime user_date = (DateTime)response;
            //system dateTime in dateTime formate
            DateTime system_date = DateTime.Now;

            //split the date , month ,year of system Date
            int system_date1 = system_date.Day;
            int system_month = system_date.Month;
            int system_year = system_date.Year;

            //split the date , month ,year of user Entered date 
            int user_date1 = user_date.Day;
            int user_month = user_date.Month;
            int user_year = user_date.Year;

            DateTime MyDate = new DateTime(user_year, user_month, user_date1, 0, 0, 0);
            DateTime SystemDate = new DateTime(system_year, system_month, system_date1, 0, 0, 0);

            int result1 = DateTime.Compare(MyDate, SystemDate);

            if (result1 < 0)
            //relationship = "i can not book";
            {
                result.IsValid = false;
                result.Value = response;
            }
            else if (result1 == 0)
            //relationship = "is I can book";
            {
                result.IsValid = true;
                result.Value = response;
            }
            else
            //relationship = "i can book";
            {
                result.IsValid = true;
                result.Value = response;
            }


            return Task.FromResult(result);
        }
    }
}