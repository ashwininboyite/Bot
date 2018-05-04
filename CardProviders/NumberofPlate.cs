using System;
using System.Threading.Tasks;
using Resturant_data.Bot.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserDeatalisModel.Bot.Models;
using Microsoft.Bot.Connector;

namespace ContosoScuba.Bot.CardProviders
{
    public class NumberofPlate : CardProvider
    {
        public override string CardName => "NumberOfPlate";

        public override bool ProvidesCard(UserDetalisData ResturantData, JObject value, string messageText, UserDetalisData ResturantData1)
        {
            return ResturantData != null
                && !string.IsNullOrEmpty(ResturantData.subDish)
                && string.IsNullOrEmpty(ResturantData.NumberOfPlates);
        }

        public override async Task<UserCardResult> GetCardResult(UserDetalisData ResturantData, JObject value, string messageText)
        {
            var NumberOfPlates = value != null ? value.Value<string>("NumberOfPlates") : messageText;

            var error = GetErrorMessage(NumberOfPlates);
            if (!string.IsNullOrEmpty(error))
                return new UserCardResult() { ErrorMessage = error };
            
            ResturantData.NumberOfPlates = NumberOfPlates;

            return new UserCardResult() { CardText = await GetCardText(ResturantData) };
        }

        private async Task<string> GetCardText(UserDetalisData ResturantData)
        {
            var replaceInfo = new Dictionary<string, string>();
            replaceInfo.Add("{{number_of_people}}", ResturantData.NumberOfPlates);

            return await base.GetCardText(replaceInfo);
        }

        private string GetErrorMessage(string userInput)
        {
            double retNum;

            if ((Double.TryParse(Convert.ToString(userInput), System.Globalization.NumberStyles.Any,
                    System.Globalization.NumberFormatInfo.InvariantInfo, out retNum))
                && retNum <= 10
                && retNum >= 1)
            {
                return string.Empty;
            }

            return "Please enter a number between 1 and 10";            
        }
    }
}