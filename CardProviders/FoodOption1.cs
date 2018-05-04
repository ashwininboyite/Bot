using System.Threading.Tasks;
using Resturant_data.Bot.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UserDeatalisModel.Bot.Models;
namespace ContosoScuba.Bot.CardProviders
{
    public class FoodOption1 : CardProvider
    {
        public override string CardName => "Foodoption";

        public override bool ProvidesCard(UserDetalisData ResturantData, JObject value, string messageText, UserDetalisData ResturantData1)
        {
            return ResturantData == null;
        }

        public override async Task<UserCardResult> GetCardResult(UserDetalisData ResturantData, JObject messageValue, string messageText)
        {
            return new UserCardResult() { CardText = await base.GetCardText() };
        }
    }
}