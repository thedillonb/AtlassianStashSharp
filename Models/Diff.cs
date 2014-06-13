using System.Collections.Generic;

namespace AtlassianStashSharp.Models
{
    public class Diff
    {
        public List<InnerDiff> Diffs { get; set; }

        public class InnerDiff
        {
            public ContentPath Source { get; set; }

            public ContentPath Destination { get; set; }

            public List<Hunk> Hunks { get; set; }

            public List<Comment> LineComments { get; set; }

            public class Hunk
            {
                public List<Segment> Segments { get; set; }

                public class Segment
                {
                    public string Type { get; set; }

                    public List<LineModel> Lines { get; set; }

                    public class LineModel
                    {
                        public int? Destination { get; set; }

                        public int? Source { get; set; }

                        public string Line { get; set; }

                        public bool Truncated { get; set; }

                        public List<long> CommentIds { get; set; }
                    }
                }
            }
        }
    }
}

