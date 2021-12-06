﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using GenshinPlayerQuery.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenshinPlayerQuery.Core
{
    internal static class GenshinApi
    {
        private const string API_SALT = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
        private const string API_APP_VERSION = "2.11.1";
        private const string API_CLIENT_TYPE = "5";
        
        private const int QUERY_PARALLELISM_DEGREE = 16;
        private const int FIRST_CHARACTER_ID = 10000000;
        private const int CHARACTER_COUNT = 70;

        public static bool GetLoginStatus()
        {
            return GetPlayerInfo("100010001", "cn_gf01").Success;
        }

        public static PlayerQueryResult GetPlayerData(string uid, string server)
        {
            ServerResponse<PlayerInfo> playerInfo = GetPlayerInfo(uid, server);
            if (!playerInfo.Success)
            {
                return new PlayerQueryResult(playerInfo.Message);
            }

            ServerResponse<JObject> spiralAbyss = GetSpiralAbyssInfo("1", uid, server);
            if (!spiralAbyss.Success)
            {
                return new PlayerQueryResult(spiralAbyss.Message);
            }

            ServerResponse<JObject> lastSpiralAbyss = GetSpiralAbyssInfo("2", uid, server);
            if (!lastSpiralAbyss.Success)
            {
                return new PlayerQueryResult(lastSpiralAbyss.Message);
            }

            List<JToken> characters = new List<JToken>();
            int[] knownRolesId = playerInfo.Data.Avatars.Select(x => x.Id).ToArray();
            ServerResponse<JObject> knownCharacters = GetCharacters(uid, server, knownRolesId);
            if (!knownCharacters.Success)
            {
                return new PlayerQueryResult(knownCharacters.Message);
            }
            characters.AddRange(knownCharacters.Data["avatars"]);

            List<JToken> unknownCharacters = Enumerable.Range(FIRST_CHARACTER_ID, CHARACTER_COUNT)
                .Except(knownRolesId)
                .AsParallel().WithDegreeOfParallelism(QUERY_PARALLELISM_DEGREE)
                .Select(roleId =>
                {
                    ServerResponse<JObject> role = GetCharacters(uid, server, roleId);
                    return role.Success ? role.Data["avatars"].First : null;
                })
                .Where(role => role != null).ToList();
            characters.AddRange(unknownCharacters);

            return new PlayerQueryResult
            {
                Success = true,
                UserId = uid,
                Server = server,
                PlayerInfo = JsonConvert.SerializeObject(playerInfo.Data),
                SpiralAbyss = $"[{spiralAbyss.Data}, {lastSpiralAbyss.Data}]",
                Roles = JsonConvert.SerializeObject(new {avatars = characters})
            };
        }

        private static ServerResponse<PlayerInfo> GetPlayerInfo(string uid, string server)
        {
            return Get<PlayerInfo>(
                $"https://api-takumi.mihoyo.com/game_record/app/genshin/api/index?role_id={uid}&server={server}");
        }

        private static ServerResponse<JObject> GetSpiralAbyssInfo(string type, string uid, string server)
        {
            return Get<JObject>(
                $"https://api-takumi.mihoyo.com/game_record/app/genshin/api/spiralAbyss?schedule_type={type}&server={server}&role_id={uid}");
        }

        private static ServerResponse<JObject> GetCharacters(string uid, string server, params int[] characterIds)
        {
            return Post<JObject>(
                "https://api-takumi.mihoyo.com/game_record/app/genshin/api/character", JsonConvert.SerializeObject(
                    new QueryRole
                    {
                        CharacterIds = characterIds.ToList(),
                        RoleId = uid,
                        Server = server
                    }));
        }

        private static ServerResponse<T> Get<T>(string url)
        {
            return Request<T>(x => x.DownloadString(url), CreateDynamicSecret(url, ""));
        }

        private static ServerResponse<T> Post<T>(string url, string data)
        {
            return Request<T>(x => x.UploadString(url, data), CreateDynamicSecret(url, data));
        }

        private static ServerResponse<T> Request<T>(Func<WebClient, string> func, string ds)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers["x-rpc-client_type"] = API_CLIENT_TYPE;
                    client.Headers["x-rpc-app_version"] = API_APP_VERSION;
                    client.Headers["DS"] = ds;
                    client.Headers["Cookie"] = MessageBus.LoginTicket;

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

        private static string CreateDynamicSecret(string url, string body)
        {
            string query = "";
            string[] urlPart = url.Split('?');
            if (urlPart.Length == 2)
            {
                string[] parameters = urlPart[1].Split('&').OrderBy(x => x).ToArray();
                query = string.Join("&", parameters);
            }

            long time = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string random = CreateRandomString(6);
            // Credits: lulu666lulu, https://github.com/Azure99/GenshinPlayerQuery/issues/20
            string check = ComputeMd5($"salt={API_SALT}&t={time}&r={random}&b={body}&q={query}");

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