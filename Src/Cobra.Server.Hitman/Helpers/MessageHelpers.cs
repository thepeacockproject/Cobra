namespace Cobra.Server.Hitman.Helpers
{
    public static class MessageHelpers
    {
        public static string MasterCraftedSilverballer(string title, string body, string contractId = null)
        {
            return contractId == null
                ? $"Silverballer|||{title}||{{baller}}|||{{baller}}||||{body}"
                : $"{{ContractId}}|||{contractId}|||Silverballer|||{title}||{{baller}}|||{{baller}}||||{body}";
        }
    }
}
