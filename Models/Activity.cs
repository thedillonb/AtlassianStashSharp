using System;

namespace AtlassianStashSharp.Models
{
    public class Activity
    {
        public long Id { get; set; }

        public long CreatedDate { get; set; }

        public User User { get; set; }

        public string Action { get; set; }

        public string CommentAction { get; set; }

        public Comment Comment { get; set; }

        public CommentAnchorModel CommentAnchor { get; set; }

        public class CommentAnchorModel
        {
            public string FromHash { get; set; }

            public string ToHash { get; set; }

            public int Line { get; set; }

            public string LineType { get; set; }

            public string FileType { get; set; }

            public string Path { get; set; }

            public string SrcPath { get; set; }

        }
    }
}

