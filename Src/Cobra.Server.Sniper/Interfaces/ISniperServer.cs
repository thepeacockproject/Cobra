using Cobra.Server.Sniper.Models;
using static Cobra.Server.Sniper.Controllers.SniperController;

namespace Cobra.Server.Sniper.Interfaces
{
    public interface ISniperServer
    {
        void Initialize();
        //AddMetrics
        List<Message> GetMessages(GetMessagesRequest request);
        int GetNewMessageCount(GetNewMessageCountRequest request);
        List<float> GetPerformanceIndexAll(GetPerformanceIndexAllRequest request);
        List<ScoreEntry> GetScores(GetScoresRequest request);
        void PutScore(PutScoreRequest request);
        void SetMessageReadStatus(SetMessageReadStatusRequest request);
        void SendTemplatedMessage(SendTemplatedMessageRequest request);
        void UpdateUserProfile(UpdateUserProfileRequest request);
    }
}
