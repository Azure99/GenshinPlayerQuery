using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    internal class PlayerInfo
    {
        [JsonProperty(PropertyName = "avatars")]
        public Avatar[] Avatars { get; set; } = new Avatar[0];

        [JsonProperty(PropertyName = "stats")]
        public PlayerStatistics PlayerStatistics { get; set; } = new PlayerStatistics();

        [JsonProperty(PropertyName = "city_explorations")]
        public CityExploration[] CityExplorations { get; set; } = new CityExploration[0];

        [JsonProperty(PropertyName = "world_explorations")]
        public WorldExploration[] WorldExplorations { get; set; } = new WorldExploration[0];
    }
}
