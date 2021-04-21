﻿using Newtonsoft.Json;

namespace GenshinPlayerQuery.Model
{
    internal class ServerResponse<T>
    {
        [JsonProperty(PropertyName = "retcode")]
        public int ReturnCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}