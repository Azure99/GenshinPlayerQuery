using GenshinPlayerQuery.Model;
using GenshinPlayerQuery.Properties;

namespace GenshinPlayerQuery.Core
{
    internal static class PlayerPageRender
    {
        public static string RenderHtml(PlayerQueryResult playerData)
        {
            return Resources.ResourceManager.GetString("index")?
                .Replace("$uid$", playerData.UserId)
                .Replace("$server$", playerData.Server)
                .Replace("$playerInfo$", playerData.PlayerInfo)
                .Replace("$spiralAbyss$", playerData.SpiralAbyss)
                .Replace("$roles$", playerData.Roles);
        }
    }
}