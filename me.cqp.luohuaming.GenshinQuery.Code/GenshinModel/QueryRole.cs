using System.Collections.Generic;
using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    internal class QueryRole
    {
        [JsonProperty(PropertyName = "character_ids")]
        public List<int> CharacterIds { get; set; }

        [JsonProperty(PropertyName = "role_id")]
        public string RoleId { get; set; }

        [JsonProperty(PropertyName = "server")]
        public string Server { get; set; }
    }
}