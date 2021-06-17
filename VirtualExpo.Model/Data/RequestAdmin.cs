using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("RequestAdmin")]
    public partial class RequestAdmin
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Link { get; set; }

        public string moto { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Telephone { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
        public int Expected_Number_Of_Exhibitor { get; set; }
        public string sponsorList { get; set; }
        public string NameOfEvent { get; set; }


    }
}
