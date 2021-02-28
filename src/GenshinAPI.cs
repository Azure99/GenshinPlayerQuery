using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using GenshinPlayerQuery.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenshinPlayerQuery
{
    static class GenshinApi
    {
        public static bool GetLoginStatus()
        {
            ServerResponse<JObject> response =
                Get<JObject>("https://api-takumi.mihoyo.com/game_record/genshin/api/index?role_id=100010001&server=cn_gf01");
            return response.ReturnCode == 0;
        }

        public static PlayerInfo GetPlayerInfo(string uid, string server)
        {
            ServerResponse<PlayerInfo> response =
                Get<PlayerInfo>(
                    $"https://api-takumi.mihoyo.com/game_record/genshin/api/index?role_id={uid}&server={server}");

            PlayerInfo playerInfo = response.Data;
            if (playerInfo != null)
            {
                playerInfo.UserId = uid;
            }
            return playerInfo;
        }

        private static ServerResponse<T> Get<T>(string url)
        {
            return Request<T>(x => x.DownloadString(url));
        }

        private static ServerResponse<T> Post<T>(string url, string data)
        {
            return Request<T>(x => x.UploadString(url, data));
        }

        private static ServerResponse<T> Request<T>(Func<WebClient, string> func)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers["x-rpc-client_type"] = "5";
                client.Headers["x-rpc-app_version"] = "2.3.0";
                client.Headers["DS"] = CreateDynamicSecret();
                client.Headers["Cookie"] = MessageBus.LoginTicket;

                string response = func(client);
                return JsonConvert.DeserializeObject<ServerResponse<T>>(response);
            }
        }

        private static string CreateDynamicSecret()
        {
            long time = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000;
            string random = CreateRandomString(6);
            string check = ComputeMd5($"salt=h8w582wxwgqvahcdkpvdhbh2w9casgfl&t={time}&r={random}");

            return $"{time},{random},{check}";
        }

        private static string CreateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder(length);

            const string randomStringTemplate = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int pos = random.Next(0, randomStringTemplate.Length);
                builder.Append(randomStringTemplate[pos]);
            }

            return builder.ToString();
        }

        private static string ComputeMd5(string content)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(content ?? ""));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in result)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
    }
}
