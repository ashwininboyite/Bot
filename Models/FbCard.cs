using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBot.Models
{
    public class FbCard
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        //try without nullvaluehandling and see what happens
        [JsonProperty("item_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemUrl { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("buttons")]
        public FbButton[] Buttons { get; set; }
    }
}