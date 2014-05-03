using System.Collections.Generic;

namespace AtlassianStashSharp.Models
{
    public class Commit
    {
        public string Id { get; set; }

        public string DisplayId { get; set; }

        public long AuthorTimestamp { get; set; }

        public string Message { get; set; }

        public CommitAuthor Author { get; set; }

        public List<CommitParent> Parents { get; set; }

        public class CommitAuthor
        {
            public string Name { get; set; }

            public string EmailAddress { get; set; }
        }

        public class CommitParent
        {
            public string Id { get; set; }

            public string DisplayId { get; set; }
        }
    }
}
