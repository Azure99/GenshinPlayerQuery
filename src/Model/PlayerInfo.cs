using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    class PlayerInfo
    {
        public string UserId = "";

        public Avatar[] Avatars { get; set; } = new Avatar[0];

        [JsonProperty(PropertyName = "stats")]
        public PlayerStatistics PlayerStatistics { get; set; } = new PlayerStatistics();

        [JsonProperty(PropertyName = "city_explorations")]
        public CityExploration[] CityExplorations { get; set; } = new CityExploration[0];

        [JsonProperty(PropertyName = "world_explorations")]
        public WorldExploration[] WorldExplorations { get; set; } = new WorldExploration[0];
    }
}
