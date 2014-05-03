using System.Collections.Generic;

namespace AtlassianStashSharp.Models
{
    public class PullRequest
    {
        public long Id { get; set; }

        public int Version { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string State { get; set; }

        public bool Open { get; set; }

        public bool Closed { get; set; }

        public long CreatedDate { get; set; }

        public long UpdatedData { get; set; }

        public PullRequestParticipant Author { get; set; }

        public List<PullRequestParticipant> Reviewers { get; set; } 

        public List<PullRequestParticipant> Participants { get; set; } 

        public Link Link { get; set; }

        public Links Links { get; set; }

        public PullRequestRef FromRef { get; set; }

        public PullRequestRef ToRef { get; set; }

        public class PullRequestRef
        {
            public string Id { get; set; }

            public RefRepository Repository { get; set; }

            public class RefRepository
            {
                public string Slug { get; set; }

                public string Name { get; set; }

                public RefProject Project { get; set; }

                public class RefProject
                {
                    public string Key { get; set; }
                }
            }
        }
    }
}
