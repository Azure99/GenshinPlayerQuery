using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenshinPlayerQuery.Model;
using GenshinPlayerQuery.Properties;

namespace GenshinPlayerQuery
{
    static class Render
    {
        public static string RenderHtml(PlayerData playerData)
        {
            return Resources.ResourceManager.GetString("index")
                .Replace("$uid$", playerData.UserId)
                .Replace("$server$", playerData.Server)
                .Replace("$playerInfo$", playerData.PlayerInfo)
                .Replace("$spiralAbyss$", playerData.SpiralAbyss)
                .Replace("$roles$", playerData.Roles);
        }
    }
}
