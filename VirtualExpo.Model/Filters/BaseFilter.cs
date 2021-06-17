using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualExpo.Model.Filters
{
    public class BaseFilter
    {
        public string Keyword { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Sort { get; set; }
    }
}
