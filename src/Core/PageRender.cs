using GenshinPlayerQuery.Model;
using GenshinPlayerQuery.Properties;

namespace GenshinPlayerQuery.Core
{
    internal static class PageRender
    {
        public static string RenderPlayerPage(PlayerQueryResult playerData)
        {
            return Resources.ResourceManager.GetString("index")?
                .Replace("$playerInfo$", playerData.PlayerInfo)
                .Replace("$spiralAbyss$", playerData.SpiralAbyss)
                .Replace("$roles$", playerData.Roles);
        }

        public static string RenderRolePage(string role)
        {
            return Resources.ResourceManager.GetString("role")?
                .Replace("$role$", role);
        }
    }
}