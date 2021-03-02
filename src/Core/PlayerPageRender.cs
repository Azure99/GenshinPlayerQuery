using GenshinPlayerQuery.Model;
using GenshinPlayerQuery.Properties;

namespace GenshinPlayerQuery.Core
{
    internal static class PlayerPageRender
    {
        public static string RenderHtml(PlayerQueryResult playerQueryResult)
        {
            return Resources.ResourceManager.GetString("index")?
                .Replace("$uid$", playerQueryResult.UserId)
                .Replace("$server$", playerQueryResult.Server)
                .Replace("$playerInfo$", playerQueryResult.PlayerInfo)
                .Replace("$spiralAbyss$", playerQueryResult.SpiralAbyss)
                .Replace("$roles$", playerQueryResult.Roles);
        }
    }
}