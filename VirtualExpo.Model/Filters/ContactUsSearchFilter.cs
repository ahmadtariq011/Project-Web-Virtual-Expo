using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Filters;

namespace VirtualExpo.Entities.Filters
{
    public class ContactUsSearchFilter : BaseFilter
    {
		public Nullable<int> Id { get; set; } 
		public string Name { get; set; } 
		public string Email { get; set; } 
		public string Telephone { get; set; } 
		public string Message { get; set; }
		public DateTime CreatedDate { get; set; }

		public bool IsRead { get; set; } = false;

	}
}