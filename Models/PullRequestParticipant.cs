namespace AtlassianStashSharp.Models
{
    public class PullRequestParticipant
    {
        public User User { get; set; }

        public string Role { get; set; }

        public bool Approved { get; set; }
    }
}
