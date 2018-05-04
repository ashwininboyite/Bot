using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBot
{
    public class Booking
    {
        public DateTime BookingDateTime { get; set; }
        public string Name { get; set; }
        public int NumPeople { get; set; }
        public string PhNum { get; set; }
        public string Email_ID { get; set; }
        public string Requests { get; set; }
    }
}