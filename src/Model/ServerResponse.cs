using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    class ServerResponse<T>
    {
        [JsonProperty(PropertyName = "retcode")]
        public int ReturnCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
