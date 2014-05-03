using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianStashSharp.Models
{
    public class Change
    {
        public string ContentId { get; set; }

        public bool Executable { get; set; }

        public int PercentUnchanged { get; set; }

        public string Type { get; set; }

        public string NodeType { get; set; }

        public bool SrcExecutable { get; set; }

        public Link Link { get; set; }

        public Links Links { get; set; }

        public ChangePath Path { get; set; }

        public ChangePath SrcPath { get; set; }

        public class ChangePath
        {
            public string Parent { get; set; }

            public List<string> Components { get; set; }

            public string Name { get; set; }

            public string Extension { get; set; }

            public new string ToString { get; set; }
        }
    }
}
