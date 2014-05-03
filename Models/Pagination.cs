using System.Collections.Generic;

namespace AtlassianStashSharp.Models
{
    public class Pagination<T>
    {
        public int Size { get; set; }

        public int Limit { get; set; }

        public bool IsLastPage { get; set; }

        public int Start { get; set; }

        public int? NextPageStart { get; set; }

        public List<T> Values { get; set; } 
    }
}
