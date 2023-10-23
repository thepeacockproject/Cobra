namespace Cobra.Server.Hitman.Models
{
    public class UserProfileChallenges
    {
        public List<CompletedChallenge> Completed { get; set; }
        public int LastUnlocked { get; set; }
        public int NumCompleted { get; set; }
    }
}
