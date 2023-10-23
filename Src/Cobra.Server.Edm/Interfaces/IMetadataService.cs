using Cobra.Server.Edm.Models;

namespace Cobra.Server.Edm.Interfaces
{
    public interface IMetadataService
    {
        OSMetadata GetMetadata();
    }
}
