using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Filters
{
    public class RequestOrganizerFilter:BaseFilter
    {
        public Nullable<int> Exhibition_Id { get; set; }
    }
}
