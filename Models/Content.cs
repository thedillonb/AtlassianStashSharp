using System.Collections.Generic;

namespace AtlassianStashSharp.Models
{
    public class BrowseContent
    {
        public List<Line> Lines { get; set; }

        public int Start { get; set; }

        public int Size { get; set; }

        public bool IsLastPage { get; set; }

        public class Line
        {
            public string Text { get; set; }
        }
    }

    public class Content
    {
        public string Node { get; set; }

        public ContentPath Path { get; set; }

        public string Type { get; set; }
    }

    public class ContentContainer
    {
        public ContentPath Path { get; set; }

        public string Revision { get; set; }

        public Pagination<Content> Children { get; set; }
    }

    public class ContentPath
    {
        public List<string> Components { get; set; }

        public string Parent { get; set; }

        public string Name { get; set; }

        public new string ToString { get; set; }
    }
}

