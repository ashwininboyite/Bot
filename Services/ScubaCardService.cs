using ContosoScuba.Bot.CardProviders;
using Resturant_data.Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserDeatalisModel.Bot.Models;
namespace ContosoScuba.Bot.Services
{
    public class ScubaCardService
    {
        public const string ResturantDataKey = "ResturantData";
        //OrderDetails orderdetails = new OrderDetails();
        List<OrderDetails> listorderdertails = new List<OrderDetails>();

        private static Lazy<List<CardProvider>> _cardHandlers = new Lazy<List<CardProvider>>(() =>
        {
            return Assembly.GetCallingAssembly().DefinedTypes
                .Where(t => (typeof(CardProvider) != t && typeof(CardProvider).IsAssignableFrom(t) && !t.IsAbstract))
                .Select(t => (CardProvider)Activator.CreateInstance(t))
                .ToList();
        });

        public async Task<UserCardResult> GetNextCardText(IDialogContext context, Activity activity)
        {
            var botdata = context.PrivateConversationData;
            string text = string.Empty;
            var userInput = activity.Text;
            UserDetalisData userResturantData = null;
            UserDetalisData userResturantData1 = null;
            if (botdata.ContainsKey(ResturantDataKey))
                userResturantData = botdata.GetValue<UserDetalisData>(ResturantDataKey);
            userResturantData1 = userResturantData;
        

            JObject jObjectValue = activity.Value as Newtonsoft.Json.Linq.JObject;

            if (jObjectValue != null)
            {
                if (((Newtonsoft.Json.Linq.JProperty)jObjectValue.First).Name == "Dish" &&
                    ((Newtonsoft.Json.Linq.JValue)((Newtonsoft.Json.Linq.JProperty)jObjectValue.First).Value).Value.ToString() == "")
                {
                    userResturantData = null;
                }
            }

            var cardProvider = _cardHandlers.Value.FirstOrDefault(c => c.ProvidesCard(userResturantData, jObjectValue, userInput, userResturantData1));

            if (cardProvider != null)
            {
                //for cards with single fields,
                //users can enter chat text (OR interact with the card's controls)
                if (userResturantData == null)
                    userResturantData = new UserDetalisData();

                var cardResult = await cardProvider.GetCardResult(userResturantData, jObjectValue, activity.Text);
                if (string.IsNullOrEmpty(cardResult.ErrorMessage))
                    botdata.SetValue<UserDetalisData>(ResturantDataKey, userResturantData);

                return cardResult;
            }
            return new UserCardResult() { ErrorMessage = "I'm sorry, I don't understand.  Please rephrase, or use the Adaptive Card to respond." };
        }

        public static async Task<string> GetCardText(string cardName)
        {
            var path = HostingEnvironment.MapPath($"/Cards/{cardName}.JSON");
            if (!File.Exists(path))
                return string.Empty;

            using (var f = File.OpenText(path))
            {
                return await f.ReadToEndAsync();
            }
        }


    }
}