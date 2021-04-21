﻿using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using GenshinPlayerQuery.Model;
using me.cqp.luohuaming.GenshinQuery.Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicInfos;

namespace GenshinPlayerQuery.Core
{
    internal static class GenshinApi
    {
        public static bool GetLoginStatus()
        {
            ServerResponse<JObject> response =
                Get<JObject>(
                    "https://api-takumi.mihoyo.com/game_record/genshin/api/index?role_id=100010001&server=cn_gf01");
            return response.ReturnCode == 0;
        }

        public static PlayerQueryResult GetPlayerData(string uid, string server)
        {
            ServerResponse<PlayerInfo> playerInfo = Get<PlayerInfo>(
                $"https://api-takumi.mihoyo.com/game_record/genshin/api/index?role_id={uid}&server={server}");
            if (playerInfo.ReturnCode != 0)
            {
                return new PlayerQueryResult(playerInfo.Message);
            }

            ServerResponse<JObject> spiralAbyss = Get<JObject>(
                $"https://api-takumi.mihoyo.com/game_record/genshin/api/spiralAbyss?schedule_type=1&server={server}&role_id={uid}");
            if (spiralAbyss.ReturnCode != 0)
            {
                return new PlayerQueryResult(spiralAbyss.Message);
            }

            ServerResponse<JObject> lastSpiralAbyss = Get<JObject>(
                $"https://api-takumi.mihoyo.com/game_record/genshin/api/spiralAbyss?schedule_type=2&server={server}&role_id={uid}");
            if (lastSpiralAbyss.ReturnCode != 0)
            {
                return new PlayerQueryResult(spiralAbyss.Message);
            }

            ServerResponse<JObject> roles = Post<JObject>(
                "https://api-takumi.mihoyo.com/game_record/genshin/api/character", JsonConvert.SerializeObject(
                    new QueryRole
                    {
                        CharacterIds = playerInfo.Data.Avatars.Select(x => x.Id).ToList(),
                        RoleId = uid,
                        Server = server
                    }));
            if (roles.ReturnCode != 0)
            {
                return new PlayerQueryResult(roles.Message);
            }

            return new PlayerQueryResult
            {
                Success = true,
                UserId = uid,
                Server = server,
                PlayerInfo = JsonConvert.SerializeObject(playerInfo.Data),
                SpiralAbyss = $"[{spiralAbyss.Data}, {lastSpiralAbyss.Data}]",
                Roles = roles.Data.ToString()
            };
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
            try
            {
                string cookie = Helper.GetCookie();
                if (string.IsNullOrWhiteSpace(cookie))
                {
                    MainSave.CQLog.Info("缺失Cookie","请在数据目录的Cookie.txt内填入自己的米游社Cookie");
                }
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers["x-rpc-client_type"] = "2";
                    client.Headers["x-rpc-app_version"] = "2.7.0";
                    client.Headers["DS"] = CreateDynamicSecret();
                    client.Headers["Cookie"] = cookie;

                    string response = func(client);
                    return JsonConvert.DeserializeObject<ServerResponse<T>>(response);
                }
            }
            catch (Exception ex)
            {
                return new ServerResponse<T>
                {
                    ReturnCode = -1,
                    Message = ex.Message
                };
            }
        }

        private static string CreateDynamicSecret()
        {
            long time = (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string random = CreateRandomString(6);
            string check = ComputeMd5($"salt=fd3ykrh7o1j54g581upo1tvpam0dsgtf&t={time}&r={random}");

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