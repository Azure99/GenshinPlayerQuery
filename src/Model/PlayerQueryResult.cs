namespace GenshinPlayerQuery.Model
{
    class PlayerQueryResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public string Server { get; set; }

        public string PlayerInfo { get; set; }

        public string SpiralAbyss { get; set; }

        public string Roles { get; set; }

        public PlayerQueryResult()
        {
        }

        public PlayerQueryResult(string message)
        {
            Message = message;
        }
    }
}