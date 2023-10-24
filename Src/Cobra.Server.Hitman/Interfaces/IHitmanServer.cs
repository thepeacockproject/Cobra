using Cobra.Server.Hitman.Models;
using static Cobra.Server.Hitman.Controllers.HitmanController;

namespace Cobra.Server.Hitman.Interfaces
{
    public interface IHitmanServer
    {
        void Initialize();
        //AddMetrics
        void CreateCompetition(CreateCompetitionRequest request);
        int ExecuteWalletTransaction(ExecuteWalletTransactionRequest request);
        List<int> GetAverageScores(BaseGetAverageScoresRequest request);
        List<ScoreEntry> GetScores(BaseGetScoresRequest request);
        ScoreComparison GetScoreComparison(GetScoreComparisonRequest request);
        Contract GetFeaturedContract(GetFeaturedContractRequest request);
        List<Message> GetMessages(GetMessagesRequest request);
        int GetNewMessageCount(GetNewMessageCountRequest request);
        GetUserOverviewData GetUserOverviewData(GetUserOverviewDataRequest request);
        int GetUserWallet(GetUserWalletRequest request);
        void InviteToCompetition(InviteToCompetitionRequest request);
        void MarkContractAsPlayed(MarkContractAsPlayedRequest request);
        //MergeUserTokens
        int PutScore(PutScoreRequest request);
        void QueueAddContract(QueueAddContractRequest request);
        void QueueRemoveContract(QueueRemoveContractRequest request);
        void ReportContract(ReportContractRequest request);
        List<Contract> SearchForContracts2(SearchForContracts2Request request);
        void SendTemplatedMessage(SendTemplatedMessageRequest request);
        void SetMessageReadStatus(SetMessageReadStatusRequest request);
        void UpdateContractLikeDislikes(UpdateContractLikeDislikesRequest request);
        //UpdateDLCInfo
        void UpdateUserInfo(UpdateUserInfoRequest request);
        //UpdateUserProfileChallenges
        //UpdateUserProfileGameStats
        //UpdateUserProfileLevelProgression
        //UpdateUserProfileSpecialRatings
        void UploadContract(UploadContractRequest request);
    }
}
