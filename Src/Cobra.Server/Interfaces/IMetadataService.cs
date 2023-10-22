using Cobra.Server.Models;

namespace Cobra.Server.Interfaces
{
    public interface IMetadataService
    {
        OSMetadata GetMetadata();
    }
}
