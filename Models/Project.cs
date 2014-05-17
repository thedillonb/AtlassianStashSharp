using System.Collections.Generic;

namespace AtlassianStashSharp.Models
{
    public class Project
    {
        public string Key { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Public { get; set; }

        public string Type { get; set; }

        public bool IsPersonal { get; set; }

        public Link Link { get; set; }

        public Dictionary<string, List<ComplexLink>> Links { get; set; }
    }
}
