using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("RequestOrganizer")]
    public partial class RequestOrganizer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string WebsiteLink { get; set; }

        public string BrandImage { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public string Telephone { get; set; }
        public int RegisterForExibitionAs { get; set; } //for check if Sponsor and Exhibitor
        [ForeignKey("Exhibition")]
        public int ExhibitionId { set; get; }
        public virtual Exhibition Exhibition { get; set; }
    }
}
