﻿using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    internal class Avatar
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "element")]
        public string Element { get; set; }

        [JsonProperty(PropertyName = "fetter")]
        public int Fetter { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }

        [JsonProperty(PropertyName = "rarity")]
        public int Rarity { get; set; }
    }
}
