namespace AtlassianStashSharp.Models
{
    public class Comment
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
}
