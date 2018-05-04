using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBot.Models
{
    public class FbAttachment
    {
        public FbAttachment()
        {
            this.Type = "template";
        }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("payload")]
        public dynamic Payload { get; set; }

        public override string ToString()
        {
            return this.Payload.ToString();
        }
    }
}