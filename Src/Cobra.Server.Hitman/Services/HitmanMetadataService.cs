using Cobra.Server.Edm.Services;
using Cobra.Server.Hitman.Interfaces;
using Cobra.Server.Hitman.Models;
using static Cobra.Server.Hitman.Controllers.HitmanController;

namespace Cobra.Server.Hitman.Services
{
    public class HitmanMetadataService : MetadataService, IHitmanMetadataService
    {
        public HitmanMetadataService()
        {
            BuildMetadata(Constants.SchemaNamespace);
        }

        protected override List<Type> GetEdmEntityTypes()
        {
            return new List<Type>
            {
                typeof(Contract),
                typeof(GetUserOverviewData),
                typeof(Message),
                typeof(ScoreComparison),
                typeof(ScoreEntry),
                typeof(UserTokenData)
            };
        }

        protected override List<Type> GetEdmFunctionImports()
        {
            return new List<Type>
            {
                typeof(CreateCompetitionRequest),
                typeof(ExecuteWalletTransactionRequest),
                typeof(GetAverageScoresRequest),
                typeof(GetDeadliestAverageScoresRequest),
                typeof(GetDeadliestScoresRequest),
                typeof(GetFeaturedContractRequest),
                typeof(GetMessagesRequest),
                typeof(GetNewMessageCountRequest),
                typeof(GetPopularAverageScoresRequest),
                typeof(GetPopularScoresRequest),
                typeof(GetRichestAverageScoresRequest),
                typeof(GetRichestScoresRequest),
                typeof(GetScoreComparisonRequest),
                typeof(GetScoresRequest),
                typeof(GetUserOverviewDataRequest),
                typeof(GetUserWalletRequest),
                typeof(InviteToCompetitionRequest),
                typeof(MarkContractAsPlayedRequest),
                typeof(MergeUserTokensRequest),
                typeof(PutScoreRequest),
                typeof(QueueAddContractRequest),
                typeof(QueueRemoveContractRequest),
                typeof(ReportContractRequest),
                typeof(SearchForContracts2Request),
                typeof(SendTemplatedMessageRequest),
                typeof(SetMessageReadStatusRequest),
                typeof(UpdateContractLikeDislikesRequest),
                typeof(UpdateDLCInfoRequest),
                typeof(UpdateUserInfoRequest),
                typeof(UpdateUserProfileChallengesRequest),
                typeof(UpdateUserProfileGameStatsRequest),
                typeof(UpdateUserProfileLevelProgressionRequest),
                typeof(UpdateUserProfileSpecialRatingsRequest),
                typeof(UploadContractRequest)
            };
        }
    }
}
