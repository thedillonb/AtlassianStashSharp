using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianStashSharp.Models
{
    public class Error
    {
        public string Context { get; set; }

        public string Message { get; set; }

        public string ExceptionName { get; set; }
    }
}
