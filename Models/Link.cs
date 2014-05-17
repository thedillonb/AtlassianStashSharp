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

    public class ComplexLink
    {
        public string Href { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Rel { get; set; }
    }

    public class Links : Dictionary<string, List<ComplexLink>>
    {
    }
}
