using System.Threading.Tasks;
using Resturant_data.Bot.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UserDeatalisModel.Bot.Models;
using System;

namespace ContosoScuba.Bot.CardProviders
{
    public class subDishes3 : CardProvider
    {
        public override string CardName => "3-People";

        public override bool ProvidesCard(UserDetalisData ResturantData, JObject value, string messageText, UserDetalisData ResturantData1)
        {
            return ResturantData != null
                    && !string.IsNullOrEmpty(ResturantData.Dish)
                    && string.IsNullOrEmpty(ResturantData.subDish);
        }

        public override async Task<UserCardResult> GetCardResult(UserDetalisData ResturantData, JObject value, string messageText)
        {
            var subDish = value != null ? value.Value<string>("subDish") : messageText;
           // var amount = value != null ? value.Value<string>("price") : messageText;
            var amount1 = value.Value<string>("price");
            var error = GetErrorMessage(subDish);
            if (!string.IsNullOrEmpty(error))
                return new UserCardResult() { ErrorMessage = error };
            int number1;
            Int32.TryParse(amount1, out number1);
            ResturantData.subDish = subDish;
            ResturantData.DishPrice = number1;
            //ResturantData.price = amount;
            return new UserCardResult() { CardText = await GetCardText(ResturantData) };
        }

        private async Task<string> GetCardText(UserDetalisData ResturantData)
        {
            var replaceInfo = new Dictionary<string, string>();
            replaceInfo.Add("{{subDish}}", ResturantData.subDish);

            return await base.GetCardText(replaceInfo);
}

        private string GetErrorMessage(string userInput)
        {
            var lowered = userInput.ToLower();
            if (lowered.Contains("corn soup")
                || lowered.Contains("panner panadi")
                || lowered.Contains("veg biryani")
                || lowered.Contains("gobi rice")
                || lowered.Contains("fired rice")
                || lowered.Contains("sizzier")
                 || lowered.Contains("carrot halwa")
                  || lowered.Contains("gulab jamun")
                   || lowered.Contains("chocolate icecream"))
            {
                return string.Empty;
            }
            return "Please enter corn soup, panner panadi, or sizzier.  You can also click on the picture for your selection ";
        }
    }
}