namespace AtlassianStashSharp.Models
{
    public class Branch
    {
        public string Id { get; set; }

        public string DisplayId { get; set; }

        public string LatestChangeset { get; set; }

        public bool IsDefault { get; set; }
    }
}
