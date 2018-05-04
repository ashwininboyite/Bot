using System.Threading.Tasks;
using ContosoScuba.Bot.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ContosoScuba.Bot.CardProviders
{
    public class dessert : CardProvider
    {
        public override string CardName => "3-People";

        public override bool ProvidesCard(UserScubaData scubaData, JObject value, string messageText)
        {
            return (scubaData != null || (IsSchool(messageText) || (value != null && IsSchool(value.Value<string>("school")))))
                && (scubaData == null || string.IsNullOrEmpty(scubaData.School));
        }

        public override async Task<ScubaCardResult> GetCardResult(UserScubaData scubaData, JObject value, string messageText)
        {
            var destination = value != null ? value.Value<string>("destination") : messageText;

            var error = GetErrorMessage(destination);
            if (!string.IsNullOrEmpty(error))
                return new ScubaCardResult() { ErrorMessage = error };

            scubaData.Destination = destination;

            return new ScubaCardResult() { CardText = await GetCardText(scubaData) };
        }

        private async Task<string> GetCardText(UserScubaData scubaData)
        {
            var replaceInfo = new Dictionary<string, string>();
            replaceInfo.Add("{{destination}}", scubaData.Destination);

            return await base.GetCardText(replaceInfo);
        }

        private string GetErrorMessage(string userInput)
        {
            var lowered = userInput.ToLower();
            if (lowered.Contains("veg")
                || lowered.Contains("gobi ")
                || lowered.Contains("biryani")
                || lowered.Contains("Rice")
                || lowered.Contains("fry")
                || lowered.Contains("Fired"))
            {
                return string.Empty;
            }
            return "Please enter veg biryani, Gobi Rice, or Fired Rice.  You can also click on the picture for your selection ";
        }
    }
}