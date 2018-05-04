using System;
using System.Collections.Generic;

namespace UserDeatalisModel.Bot.Models
{
    [Serializable]
    public class UserDetalisData
    {
        public string Dish { get; set; }
        public string subDish { get; set; }
        //public int? NumberOfPlates { get; set; }
        public string NumberOfPlates { get; set; }
        public string Date { get; set; }
        public string price { get; set; }
        public int DishPrice { get; set; }
        public int TotalPrice { get; set; }
    }

    [Serializable]
    public class OrderDetails
    {
        public string listmenu1 { get; set; }
        public string listmenu2 { get; set; }
        public string listmenu3 { get; set; }

        public OrderDetails(string list1, string list2, string list3)
        {
            listmenu1 = list1;
            listmenu2 = list2;
            listmenu3 = list3;
        }
    }
}