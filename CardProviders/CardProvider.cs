using Resturant_data.Bot.Models;
using ContosoScuba.Bot.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserDeatalisModel.Bot.Models;

namespace ContosoScuba.Bot.CardProviders
{
    public abstract class CardProvider
    {
        public abstract string CardName { get; }
        //public string CardName1 { get; private set; }

        public abstract bool ProvidesCard(UserDetalisData ResturantData, JObject value, string messageText, UserDetalisData ResturantData1);
        
        public abstract Task<UserCardResult> GetCardResult(UserDetalisData ResturantData, JObject value, string messageText);

        protected async Task<string> GetCardText(Dictionary<string, string> replaceInfo = null)
        {
            string value = string.Empty;
            string cardJson;
            if (replaceInfo != null)
            {
                foreach (var replaceKvp in replaceInfo)
                    value = replaceKvp.Value;
            }
            cardJson = await ScubaCardService.GetCardText(CardName);
           
            if (string.IsNullOrEmpty(cardJson))
                return string.Empty;

            if (replaceInfo == null)
                return cardJson;

            foreach (var replaceKvp in replaceInfo)
                cardJson = cardJson.Replace(replaceKvp.Key, replaceKvp.Value);

            return cardJson;
        }
    }
}