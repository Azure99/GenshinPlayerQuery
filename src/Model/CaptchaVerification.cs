using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinPlayerQuery.Model
{
    internal class CaptchaVerification
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "geetest_challenge")]
        public string Challenge { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "geetest_seccode")]
        public string SecCode { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "geetest_validate")]
        public string Validate { get; set; }
    }
}
