using System;

namespace AtlassianStashSharp.Models
{
    public class BuildStatus
    {
        public string State { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public long DateAdded { get; set; }
    }

    public class BuildStatusStats
    {
        public int Successful { get; set; }
        public int InProgress { get; set; }
        public int Failed { get; set; }
    }
}

