using Cobra.Server.Shared.Enums;
using Cobra.Server.Sniper.Interfaces;
using Cobra.Server.Sniper.Models;
using static Cobra.Server.Sniper.Controllers.SniperController;

namespace Cobra.Server.Sniper.Services
{
    public class MockedSniperServer : ISniperServer
    {
        private readonly List<ScoreEntry> _mockedGetScoresResponse = new()
        {
            new ScoreEntry
            {
                UserId = "76561197989140534",
                Rank = 1,
                Score = 1989
            },
            new ScoreEntry
            {
                UserId = "76561198025604927",
                Rank = 2,
                Score = 1989
            },
            new ScoreEntry
            {
                UserId = "76561198215015615",
                Rank = 3,
                Score = 1989
            },
            new ScoreEntry
            {
                UserId = "76561198161220058",
                Rank = 4,
                Score = 1989
            }
        };

        private readonly List<float> _mockedPerformanceIndexAllResponse = new()
        {
            1, //Global
            0.65f, //Global percentage
            1, //National
            0.55f, //National percentage
            1, //Friends
            0.75f //Friends percentage
        };

        public void Initialize()
        {
            //Do nothing
        }

        public List<Message> GetMessages(GetMessagesRequest request)
        {
            var templateData = "Diana, she..." +
                               "|||" +
                               "Always talks about him.<BR><BR>Way back when they first worked together.";

            var messages = Enum
                .GetValues<EMessageCategory>()
                .Skip(request.Skip)
                .Take(request.Limit)
                .Select((x, i) => new Message
                {
                    Id = 5000 + i,
                    Category = x,
                    FromId = "LennardF1989",
                    TextTemplateId = 0,
                    TemplateData = templateData,
                    TimestampUTC = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                })
                .ToList();

            return messages;
        }

        public int GetNewMessageCount(GetNewMessageCountRequest request)
        {
            return 10;
        }

        public List<float> GetPerformanceIndexAll(GetPerformanceIndexAllRequest request)
        {
            return _mockedPerformanceIndexAllResponse;
        }

        public List<ScoreEntry> GetScores(GetScoresRequest request)
        {
            return _mockedGetScoresResponse;
        }

        public void PutScore(PutScoreRequest request)
        {
            //Do nothing
        }

        public void SendTemplatedMessage(SendTemplatedMessageRequest request)
        {
            //Do nothing
        }

        public void SetMessageReadStatus(SetMessageReadStatusRequest request)
        {
            //Do nothing
        }

        public void UpdateUserProfile(UpdateUserProfileRequest request)
        {
            //Do nothing
        }
    }
}
