using System.Text.Json;
using Cobra.Server.Hitman.Interfaces;
using Cobra.Server.Hitman.Models;
using Cobra.Server.Shared.Interfaces;
using static Cobra.Server.Hitman.Controllers.HitmanController;

namespace Cobra.Server.Hitman.Services
{
    public class LocalHitmanServer : IHitmanServer
    {
        private sealed class UserProfile
        {
            public class PlayedContract
            {
                public int Plays { get; set; }
                public int Score { get; set; }
            }

            public string UserId { get; set; }
            public string DisplayName { get; set; }
            public int TotalEarnings { get; set; }
            public int WalletAmount { get; set; }
            public int ContractsCreated { get; set; }
            public Dictionary<string, PlayedContract> PlayedContracts { get; set; }
            public HashSet<string> ContractQueue { get; set; }
            public HashSet<string> Friends { get; set; }

            public UserProfile()
            {
                PlayedContracts = new Dictionary<string, PlayedContract>();
                ContractQueue = new HashSet<string>();
                Friends = new HashSet<string>();
            }
        }

        private static readonly string _userProfilePath = Path.Combine("Data", "userprofile.json");

        private static readonly List<int> _mockedGetAverageScoresResponse = new()
        {
            0, //World Average
            0, //Country Average
            0, //Friends Average
            0 //Score: Deadliest / Richest / Most Popular Assassin
        };

        private static readonly List<ScoreEntry> _mockedGetScoresResponse = new();

        private static readonly object _profileLock = new();

        private readonly ISimpleLogger _logger;
        private readonly IContractsService _contractsService;

        private UserProfile _userProfile;

        public LocalHitmanServer(
            ISimpleLogger logger,
            IContractsService contractsService
        )
        {
            _logger = logger;
            _contractsService = contractsService;
        }

        public void Initialize()
        {
            Directory.CreateDirectory(Path.Combine("Data", "Contracts"));

            _contractsService.RebuildCache();

            _userProfile = LoadUserProfile();
        }

        public void CreateCompetition(CreateCompetitionRequest request)
        {
            //Do nothing
        }

        public int ExecuteWalletTransaction(ExecuteWalletTransactionRequest request)
        {
            SaveUserProfile(() =>
            {
                //NOTE: This would allow wallet amount to go negative if the game doesn't do the proper validations.
                _userProfile.WalletAmount -= request.Amount;
            });

            return _userProfile.WalletAmount;
        }

        public List<int> GetAverageScores(BaseGetAverageScoresRequest request)
        {
            return _mockedGetAverageScoresResponse;
        }

        public List<ScoreEntry> GetScores(BaseGetScoresRequest request)
        {
            return _mockedGetScoresResponse;
        }

        public List<Message> GetMessages(GetMessagesRequest request)
        {
            return new List<Message>();
        }

        public Contract GetFeaturedContract(GetFeaturedContractRequest request)
        {
            var contract = _contractsService
                .GetContracts(new SearchForContracts2Request
                {
                    LevelIndex = request.LevelIndex,
                    CheckpointId = -1,
                    Difficulty = -1,
                    StartIndex = 0,
                    Range = 1
                }, contract => GetPlayedContract(contract.Id)?.Plays == 0)
                .FirstOrDefault();

            if (contract != null)
            {
                contract.UserScore = GetPlayedContract(contract.Id)?.Score ?? 0;
            }

            return contract;
        }

        public int GetNewMessageCount(GetNewMessageCountRequest request)
        {
            return 0;
        }

        public Task<GetUserOverviewData> GetUserOverviewData(GetUserOverviewDataRequest request)
        {
            return Task.FromResult(new GetUserOverviewData
            {
                WalletAmount = _userProfile.WalletAmount,
                ContractPlays = _userProfile.PlayedContracts.Sum(x => x.Value.Plays),
                ContractsCreated = _userProfile.ContractsCreated
            });
        }

        public Task<int> GetUserWallet(GetUserWalletRequest request)
        {
            return Task.FromResult(_userProfile.WalletAmount);
        }

        public void InviteToCompetition(InviteToCompetitionRequest request)
        {
            //Do nothing
        }

        public void MarkContractAsPlayed(MarkContractAsPlayedRequest request)
        {
            SaveUserProfile(() =>
            {
                var playedContract = GetOrAddPlayedContract(request.ContractId);

                playedContract.Plays++;
            });
        }

        public int PutScore(PutScoreRequest request)
        {
            var difference = 0;

            SaveUserProfile(() =>
            {
                var playedContract = GetOrAddPlayedContract(request.LeaderboardId);

                difference = request.Score - playedContract.Score;

                if (difference > 0)
                {
                    _userProfile.TotalEarnings += difference;
                    _userProfile.WalletAmount += difference;
                }

                playedContract.Score = Math.Max(request.Score, playedContract.Score);
            });

            return difference;
        }

        public void QueueAddContract(QueueAddContractRequest request)
        {
            SaveUserProfile(() => { _userProfile.ContractQueue.Add(request.ContractId); });
        }

        public void QueueRemoveContract(QueueRemoveContractRequest request)
        {
            SaveUserProfile(() => { _userProfile.ContractQueue.Remove(request.ContractId); });
        }

        public void ReportContract(ReportContractRequest request)
        {
            //Do nothing
        }

        public List<Contract> SearchForContracts2(SearchForContracts2Request request)
        {
            var contracts = _contractsService
                .GetContracts(request, contract =>
                {
                    return request.View switch
                    {
                        0 => true,
                        10 => _userProfile.ContractQueue.Contains(contract.Id),
                        20 => _userProfile.PlayedContracts.ContainsKey(contract.Id),
                        50 => _userProfile.UserId == contract.UserId,
                        60 => true, //NOTE: For convenience in the UI, should actually be false.
                        70 => _userProfile.Friends.Contains(contract.UserId),
                        _ => false
                    };
                })
                .ToList();

            contracts.ForEach(x =>
            {
                var playedContract = GetPlayedContract(x.Id);

                if (playedContract == null)
                {
                    return;
                }

                x.UserScore = playedContract.Score;
                x.Plays = playedContract.Plays;
            });

            return contracts;
        }

        public void SetMessageReadStatus(SetMessageReadStatusRequest request)
        {
            //Do nothing
        }

        public void UpdateContractLikeDislikes(UpdateContractLikeDislikesRequest request)
        {
            //Do nothing
        }

        public void SendTemplatedMessage(SendTemplatedMessageRequest request)
        {
            //Do nothing
        }

        public Task UpdateUserInfo(UpdateUserInfoRequest request)
        {
            SaveUserProfile(() =>
            {
                _userProfile.UserId = request.UserId;
                _userProfile.DisplayName = request.DisplayName;

                request.Friends.ForEach(x => _userProfile.Friends.Add(x));
            });

            return Task.CompletedTask;
        }

        public void UploadContract(UploadContractRequest request)
        {
            var contractId = _contractsService.CreateContract(request);

            SaveUserProfile(() =>
            {
                var playedContract = GetOrAddPlayedContract(contractId);
                playedContract.Score = request.Score;

                _userProfile.ContractsCreated++;
            });
        }

        public ScoreComparison GetScoreComparison(GetScoreComparisonRequest request)
        {
            var playedContract = GetPlayedContract(request.LeaderboardId);

            if (playedContract == null)
            {
                return null;
            }

            return new ScoreComparison
            {
                FriendName = _userProfile.UserId,
                FriendScore = playedContract.Score,
                CountryAverage = 0,
                WorldAverage = 0
            };
        }

        private UserProfile LoadUserProfile()
        {
            lock (_profileLock)
            {
                try
                {
                    if (File.Exists(_userProfilePath))
                    {
                        return JsonSerializer.Deserialize<UserProfile>(
                            File.ReadAllText(_userProfilePath)
                        );
                    }
                }
                catch
                {
                    if (File.Exists(_userProfilePath))
                    {
                        File.Move(
                            _userProfilePath,
                            $"{_userProfilePath}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}"
                        );
                    }

                    _logger.WriteLine("Failed to load user profile! Backing up original and creating new one instead.");
                }

                return new UserProfile();
            }
        }

        private void SaveUserProfile(Action scope)
        {
            lock (_profileLock)
            {
                scope();

                File.WriteAllText(_userProfilePath, JsonSerializer.Serialize(_userProfile));
            }
        }

        private UserProfile.PlayedContract GetPlayedContract(string contractId)
        {
            return _userProfile.PlayedContracts.TryGetValue(contractId, out var playedContract)
                ? playedContract
                : null;
        }

        private UserProfile.PlayedContract GetOrAddPlayedContract(string contractId)
        {
            var playedContract = GetPlayedContract(contractId);

            if (playedContract != null)
            {
                return playedContract;
            }

            playedContract = new UserProfile.PlayedContract();

            _userProfile.PlayedContracts[contractId] = playedContract;

            return playedContract;
        }
    }
}
