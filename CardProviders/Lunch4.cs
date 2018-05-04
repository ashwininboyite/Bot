using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_data.Bot.Models;
using Newtonsoft.Json.Linq;
using UserDeatalisModel.Bot.Models;

namespace ContosoScuba.Bot.CardProviders
{
    public class Lunch4 : CardProvider
    {
        public override string CardName => "7-Weather";
        
        public override bool ProvidesCard(UserDetalisData ResturantData, JObject value, string messageText, UserDetalisData ResturantData1)
        {
            return ResturantData != null 
                && !string.IsNullOrEmpty(ResturantData.NumberOfPlates)
                && string.IsNullOrEmpty(ResturantData.Date);            
        }

        public override async Task<UserCardResult> GetCardResult(UserDetalisData ResturantData, JObject value, string messageText)
        {
            
            return new UserCardResult() { CardText = await base.GetCardText() };

        }

         

    }
}