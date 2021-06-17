
using System;
using System.Collections.Generic;
using System.Text;
using VirtualExpo.Model.Filters;

namespace VirtualExpo.Models.Filters
{
    public class UserSearchFilter : BaseFilter
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
