using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace  Resturant_data.Bot.Models
{
    public class UserCardResult
    {
        public string ErrorMessage { get; set; }
        public string CardText { get; set; }
    }
}