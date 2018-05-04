using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using Newtonsoft.Json;
using ContosoScuba.Bot.Services;
using RestaurantBot;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ContosoScuba.Bot.Dialogs
{
    [Serializable]
    public class RootDialog : LuisDialog<Object>, IDialog<object>
    {
        private const string ERROR =
            "I didn't understand please respond with \"View menu\"or \"Thank you\"";

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            //return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            string text = string.IsNullOrEmpty(activity.Text) ? string.Empty : activity.Text.ToLower();

            IMessageActivity nextMessage = null;

            if (!string.IsNullOrEmpty(text))
            {
                nextMessage = await GetMessageFromText(context, activity, text);
            }

            if (nextMessage == null)
                nextMessage = await GetNextScubaMessage(context, activity);

            await context.PostAsync(nextMessage);

            //if (activity.Value != null && ((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JContainer)activity.Value).First).Name == "date")
            //{
            //    MainDialog main = new MainDialog();
            //    await main.Display(context);
            //}

            //context.Wait(MessageReceivedAsync);
        }

        private async Task<IMessageActivity> GetMessageFromText(IDialogContext context, Activity activity, string text)
        {
            IMessageActivity nextMessage = null;

            /*if (text.Contains("wildlife"))
            {
                return nextMessage = await GetCard(activity, "Wildlife");
            }*/
            if (text == "Thank you"
                    || text == "Thanks"
                    || text == "Thankyou"
                    || text == "thanks"
                    || text == "thank you")
            {
                return nextMessage = await GetCard(activity, "FoodOption1");
            }
            /* else if (text.Contains("danger"))
             {
                 return nextMessage = await GetCard(activity, "Danger");
             }*/
            else if (text == "hi"
                     || text == "hello"
                     || text == "reset"
                     || text == "start over"
                     || text == "restart")
            {
                //clear conversation data, since the user has decided to restart
                context.PrivateConversationData.Clear();
                nextMessage = await GetCard(activity, "0-Welcome");
            }

            return nextMessage;
        }

        private async Task<IMessageActivity> GetCard(Activity activity, string cardName)
        {
            var cardText = await ScubaCardService.GetCardText(cardName);
            return GetCardReply(activity, cardText);
        }

        private async Task<IMessageActivity> GetNextScubaMessage(IDialogContext context, Activity activity)
        {
            var resultInfo = await new ScubaCardService().GetNextCardText(context, activity);
            if (!string.IsNullOrEmpty(resultInfo.ErrorMessage))
                return activity.CreateReply(resultInfo.ErrorMessage);

            return GetCardReply(activity, resultInfo.CardText);
        }

        public static Activity GetCardReply(Activity activity, string cardText)
        {
            var reply = JsonConvert.DeserializeObject<Activity>(cardText);
            if (reply.Attachments == null)
                reply.Attachments = new List<Attachment>();

            var tempReply = activity.CreateReply("");
            reply.ChannelId = tempReply.ChannelId;
            reply.Timestamp = tempReply.Timestamp;
            reply.From = tempReply.From;
            reply.Conversation = tempReply.Conversation;
            reply.Recipient = tempReply.Recipient;
            reply.Id = tempReply.Id;
            reply.ReplyToId = tempReply.ReplyToId;
            if (reply.Type == null)
                reply.Type = ActivityTypes.Message;

            return reply;
        }
    }
}