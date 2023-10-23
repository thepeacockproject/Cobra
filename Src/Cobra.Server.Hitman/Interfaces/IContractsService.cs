using Cobra.Server.Hitman.Controllers;
using Cobra.Server.Hitman.Models;

namespace Cobra.Server.Hitman.Interfaces
{
    public interface IContractsService
    {
        void RebuildCache();
        string CreateContract(HitmanController.UploadContractRequest request);
        string CreateContract(string compressedBase64Contract);
        void RemoveContract(string contractId);
        IEnumerable<Contract> GetContracts(HitmanController.SearchForContracts2Request request, Func<Contract, bool> additionalFilter = null);
        string GetShareableContractLink(string contractId);
        string GetLevelName(Contract contract);
        string GetDifficultyName(Contract contract);
    }
}
