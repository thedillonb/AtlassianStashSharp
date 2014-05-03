using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianStashSharp.Models
{
    public class Link
    {
        public string Url { get; set; }

        public string Rel { get; set; }
    }

    public class Links : Dictionary<string, List<Link>>
    {
        public class Link
        {
            public string Href { get; set; }

            public string Name { get; set; }

            public string Url { get; set; }

            public string Rel { get; set; }
        }

    }
}
