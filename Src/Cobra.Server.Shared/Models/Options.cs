namespace Cobra.Server.Shared.Models
{
    public class Options
    {
        public enum EGameService
        {
            Mocked = 0,
            Local,
            Public
        }

        public enum ESteamService
        {
            None = 0,
            Mocked,
            GameServer,
            WebApi
        }

        public bool FixAddMetricsContentType { get; set; } = false;
        public bool EnableRequestLogging { get; set; } = false;
        public bool EnableRequestBodyLogging { get; set; } = false;
        public bool EnableResponseBodyLogging { get; set; } = false;

        public int JwtTokenExpirationInSeconds { get; set; } = 60 * 60 * 8; //NOTE: 8 hours
        public string JwtSignKey { get; set; } = Guid.NewGuid().ToString();

        public EGameService GameService { get; set; } = EGameService.Mocked;
        public ESteamService SteamService { get; set; } = ESteamService.None;
        public string SteamWebApiKey { get; set; } = string.Empty;

        public int MockedWalletAmount { get; set; } = 1337;
        public string MockedContractSteamId { get; set; } = "76561198161220058";
        public ulong MockedSteamServiceSteamId { get; set; } = 76561197989140534;
    }
}
