using Cobra.Server.Hitman.Controllers;
using Cobra.Server.Hitman.Interfaces;
using Cobra.Server.Hitman.Models;
using Cobra.Server.Shared.Interfaces;

namespace Cobra.Server.Hitman.Services
{
    public class HitmanServer : IHitmanServer
    {
        private readonly IUserService _userService;
        private readonly IHitmanUserService _hitmanUserService;

        public HitmanServer(
            IUserService userService,
            IHitmanUserService hitmanUserService
        )
        {
            _userService = userService;
            _hitmanUserService = hitmanUserService;
        }

        public void Initialize()
        {
            //Do nothing
        }

        public void CreateCompetition(HitmanController.CreateCompetitionRequest request)
        {
            //Do nothing
        }

        public int ExecuteWalletTransaction(HitmanController.ExecuteWalletTransactionRequest request)
        {
            return 0;
        }

        public List<int> GetAverageScores(HitmanController.BaseGetAverageScoresRequest request)
        {
            return new List<int> { 0, 0, 0, 0 };
        }

        public List<ScoreEntry> GetScores(HitmanController.BaseGetScoresRequest request)
        {
            return new List<ScoreEntry>();
        }

        public ScoreComparison GetScoreComparison(HitmanController.GetScoreComparisonRequest request)
        {
            return null;
        }

        public Contract GetFeaturedContract(HitmanController.GetFeaturedContractRequest request)
        {
            return null;
        }

        public List<Message> GetMessages(HitmanController.GetMessagesRequest request)
        {
            return new List<Message>();
        }

        public int GetNewMessageCount(HitmanController.GetNewMessageCountRequest request)
        {
            return 0;
        }

        public async Task<GetUserOverviewData> GetUserOverviewData(HitmanController.GetUserOverviewDataRequest request)
        {
            var userId = _userService.GetCurrentUserId();

            return await _hitmanUserService.GetUserOverviewData(userId);
        }

        public async Task<int> GetUserWallet(HitmanController.GetUserWalletRequest request)
        {
            var userId = _userService.GetCurrentUserId();

            return await _hitmanUserService.GetUserWallet(userId) ?? 0;
        }

        public void InviteToCompetition(HitmanController.InviteToCompetitionRequest request)
        {
            //Do nothing
        }

        public void MarkContractAsPlayed(HitmanController.MarkContractAsPlayedRequest request)
        {
            //Do nothing
        }

        public int PutScore(HitmanController.PutScoreRequest request)
        {
            return 0;
        }

        public void QueueAddContract(HitmanController.QueueAddContractRequest request)
        {
            //Do nothing
        }

        public void QueueRemoveContract(HitmanController.QueueRemoveContractRequest request)
        {
            //Do nothing
        }

        public void ReportContract(HitmanController.ReportContractRequest request)
        {
            //Do nothing
        }

        public List<Contract> SearchForContracts2(HitmanController.SearchForContracts2Request request)
        {
            return new List<Contract>();
        }

        public void SendTemplatedMessage(HitmanController.SendTemplatedMessageRequest request)
        {
            //Do nothing
        }

        public void SetMessageReadStatus(HitmanController.SetMessageReadStatusRequest request)
        {
            //Do nothing
        }

        public void UpdateContractLikeDislikes(HitmanController.UpdateContractLikeDislikesRequest request)
        {
            //Do nothing
        }

        public Task UpdateUserInfo(HitmanController.UpdateUserInfoRequest request)
        {
            var userId = _userService.GetCurrentUserId();

            var friends = request.Friends
                .Select(ulong.Parse)
                .ToList();

            //TODO: Validate if country is a valid id, this is a non-standard and will have to be extracted from the game.

            _hitmanUserService.UpdateUserInfo(
                userId,
                request.DisplayName,
                request.Country,
                friends
            );

            return Task.CompletedTask;
        }

        public void UploadContract(HitmanController.UploadContractRequest request)
        {
            //Do nothing
        }
    }
}
