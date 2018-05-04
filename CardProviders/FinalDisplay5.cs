//weather.cs file after adding the prompt dialog

using Resturant_data.Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using UserDeatalisModel.Bot.Models;

namespace ContosoScuba.Bot.CardProviders
{
    public class FinalDisplay5 : CardProvider
    {
        public override string CardName => "Finaldisplay";

        List<string> list1 = new List<string>();
        List<string> count = new List<string>();
        private IDialogContext context;
        public static List<UserDetalisData> listUserResturantData = new List<UserDetalisData>();

        public override bool ProvidesCard(UserDetalisData ResturantData, JObject value, string messageText, UserDetalisData ResturantData1)
        {
            savedata(ResturantData1);
            //orderDetails1 = orderdetails;

            return ResturantData != null
                && !string.IsNullOrEmpty(ResturantData.NumberOfPlates)
                && string.IsNullOrEmpty(ResturantData.Date);
        }

        private void savedata(UserDetalisData ResturantData1)
        {
            //if (ResturantData == null)
            //    list1.Clear();
            if (ResturantData1 != null && ResturantData1.subDish != null && ResturantData1.NumberOfPlates != null && ResturantData1.Dish != null)
            {
                if (listUserResturantData != null && listUserResturantData.Any(x => x.subDish == ResturantData1.subDish))
                {
                    int number1 = 0, number2 = 0, sum;
                    Int32.TryParse(ResturantData1.NumberOfPlates, out number1);
                    //Int32.Parse(count[])+Int32.Parse(count)
                    string numb = (from list in listUserResturantData
                                   where list.subDish.Equals(ResturantData1.subDish)
                                   select list.NumberOfPlates).FirstOrDefault();

                    Int32.TryParse(numb, out number2);
                    sum = number1 + number2;
                    numb = sum.ToString();

                    foreach (var item in listUserResturantData.Where(x => x.subDish == ResturantData1.subDish))
                    {
                        item.NumberOfPlates = numb;
                        item.TotalPrice = item.TotalPrice + ResturantData1.DishPrice * number1;
                    }
                    //ResturantData1.TotalPrice = ResturantData1.TotalPrice + ResturantData1.DishPrice * number1;
                }
                else
                {
                    int number3;
                    Int32.TryParse(ResturantData1.NumberOfPlates, out number3) ;
                    ResturantData1.TotalPrice = ResturantData1.DishPrice * number3;
                    listUserResturantData.Add(ResturantData1);
                }
            }

        }

        public override async Task<UserCardResult> GetCardResult(UserDetalisData ResturantData, JObject value, string messageText)
        {
            return new UserCardResult() { CardText = await GetCardText(ResturantData) };
        }

        private async Task<string> GetCardText(UserDetalisData ResturantData)
        {
            var replaceInfo = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();

            foreach (var item in listUserResturantData)
            {
                sb.Append(item.subDish + "\t\t" + item.NumberOfPlates + "\t\t"+item.TotalPrice+"\n\n");
            }
            replaceInfo.Add("{{subDish}}", sb.ToString());
           // listUserResturantData.Clear();
            return await base.GetCardText(replaceInfo);
        }
    }
}