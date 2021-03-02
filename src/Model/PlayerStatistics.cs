using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    internal class PlayerStatistics
    {
        [JsonProperty(PropertyName = "active_day_number")]
        public int ActiveDayNumber { get; set; } = 1;

        [JsonProperty(PropertyName = "achievement_number")]
        public int AchievementNumber { get; set; }

        [JsonProperty(PropertyName = "win_rate")]
        public int WinRate { get; set; }

        [JsonProperty(PropertyName = "anemoculus_number")]
        public int AnemoculusNumber { get; set; }

        [JsonProperty(PropertyName = "geoculus_number")]
        public int GeoculusNumber { get; set; }

        [JsonProperty(PropertyName = "avatar_number")]
        public int AvatarNumber { get; set; }

        [JsonProperty(PropertyName = "way_point_number")]
        public int WayPointNumber { get; set; }

        [JsonProperty(PropertyName = "domain_number")]
        public int DomainNumber { get; set; }

        [JsonProperty(PropertyName = "spiral_abyss")]
        public string SpiralAbyss { get; set; }

        [JsonProperty(PropertyName = "precious_chest_number")]
        public int PreciousChestNumber { get; set; }

        [JsonProperty(PropertyName = "luxurious_chest_number")]
        public int LuxuriousChestNumber { get; set; }

        [JsonProperty(PropertyName = "exquisite_chest_number")]
        public int ExquisiteChestNumber { get; set; }

        [JsonProperty(PropertyName = "common_chest_number")]
        public int CommonChestNumber { get; set; }
    }
}