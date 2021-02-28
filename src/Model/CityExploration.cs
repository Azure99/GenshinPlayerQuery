using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    class CityExploration
    {
        public int Id { get; set; }

        public int Level { get; set; }

        [JsonProperty(PropertyName = "exploration_percentage")]
        public int ExplorationPercentage { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }
    }
}
