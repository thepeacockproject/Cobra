namespace Cobra.Server.Interfaces
{
    public interface ISteamService
    {
        Task<bool> AuthenticateUser(byte[] authTicketDataBytes, ulong steamId);
    }
}
