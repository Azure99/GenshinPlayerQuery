using GenshinPlayerQuery.Model;
using GenshinPlayerQuery.Properties;

namespace GenshinPlayerQuery
{
    static class Render
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
