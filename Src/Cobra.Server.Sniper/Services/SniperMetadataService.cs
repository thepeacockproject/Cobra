using Cobra.Server.Edm.Services;
using Cobra.Server.Sniper.Interfaces;
using Cobra.Server.Sniper.Models;
using static Cobra.Server.Sniper.Controllers.SniperController;

namespace Cobra.Server.Sniper.Services
{
    public class SniperMetadataService : MetadataService, ISniperMetadataService
    {
        public SniperMetadataService()
        {
            BuildMetadata(Constants.SchemaNamespace);
        }

        protected override List<Type> GetEdmEntityTypes()
        {
            return new List<Type>
            {
                typeof(ScoreEntry),
                typeof(Message)
            };
        }

        protected override List<Type> GetEdmFunctionImports()
        {
            return new List<Type>
            {
                typeof(GetMessagesRequest),
                typeof(GetNewMessageCountRequest),
                typeof(GetPerformanceIndexAllRequest),
                typeof(GetScoresRequest),
                typeof(PutScoreRequest),
                typeof(SendTemplatedMessageRequest),
                typeof(SetMessageReadStatusRequest),
                typeof(UpdateUserProfileRequest)
            };
        }
    }
}
