using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinPlayerQuery.Model
{
    internal class CaptchaChallenge
    {
        [JsonProperty(PropertyName = "challenge")]
        public string Challenge { get; set; }

        [JsonProperty(PropertyName = "gt")]
        public string GT { get; set; }

        [JsonProperty(PropertyName = "new_captcha")]
        public int NewCaptcha { get; set; }

        [JsonProperty(PropertyName = "success")]
        public int Success { get; set; }
    }
}
