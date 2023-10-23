using Cobra.Server.Edm.Interfaces;

namespace Cobra.Server.Edm.Models.Response
{
    public class BaseEntryResponse<T> : BaseResponse<EntryObject<T>>
        where T : IEdmEntity
    {
        //Do nothing
    }
}
