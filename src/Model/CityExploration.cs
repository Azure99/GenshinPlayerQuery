using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    class CityExploration
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }

        [JsonProperty(PropertyName = "exploration_percentage")]
        public int ExplorationPercentage { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
