using System.Collections.Generic;

namespace AtlassianStashSharp.Models
{
    public class PostComment
    {
        public string Text { get; set; }

        public CommentAnchor Anchor { get; set; }

        public CommentParent Parent { get; set; }

        public class CommentParent
        {
            public int Id { get; set; }
        }

        public class CommentAnchor
        {
            public int Line { get; set; }

            public string LineType { get; set; }

            public string FileType { get; set; }

            public string Path { get; set; }

            public string SrcPath { get; set; }
        }
    }


    public class Comment
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public User Author { get; set; }

        public long CreatedDate { get; set; }

        public long UpdatedDate { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
