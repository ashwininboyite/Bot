using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBot.Models
{
    public class FbAttachmentPayload
    {
        public FbAttachmentPayload()
        {
            this.TemplateType = "generic";
        }

        [JsonProperty("template_type")]
        public string TemplateType { get; set; }

        [JsonProperty("elements")]
        public FbCard[] Elements { get; set; }

    }
}