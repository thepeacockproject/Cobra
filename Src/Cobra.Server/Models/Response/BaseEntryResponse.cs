using Cobra.Server.Interfaces;

namespace Cobra.Server.Models.Response
{
    public class BaseEntryResponse<T> : BaseResponse<EntryObject<T>>
        where T : IEdmEntity
    {
        //Do nothing
    }
}
