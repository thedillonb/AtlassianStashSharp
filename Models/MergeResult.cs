using System.Collections.Generic;

namespace AtlassianStashSharp.Models
{
    public class MergeResult
    {
        public bool CanMerge { get; set; }

        public bool Conflicted { get; set; }

        public List<MergeVeto> Vetoes { get; set; }

        public class MergeVeto
        {
            public string SummaryMessage { get; set; }

            public string DetailedMessage { get; set; }
        }
    }
}

