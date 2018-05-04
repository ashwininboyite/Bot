using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBot.Models
{
    public class FbChannelData
    {
        [JsonProperty("attachment")]
        public FbAttachment Attachment { get; internal set; }
    }
}