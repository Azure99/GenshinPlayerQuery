using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    class QueryRole
    {
        [JsonProperty(PropertyName = "character_ids")]
        public List<int> CharacterIds { get; set; }

        [JsonProperty(PropertyName = "role_id")]
        public string RoleId { get; set; }

        [JsonProperty(PropertyName = "server")]
        public string Server { get; set; }
    }
}
