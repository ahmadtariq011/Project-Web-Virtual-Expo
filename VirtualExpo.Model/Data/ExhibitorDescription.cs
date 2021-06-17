using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("ExhibitorDescription")]
    public partial class ExhibitorDescription
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Moto { get; set; }

        public string Picture { get; set; }
        [ForeignKey("Exhibition")]
        public int Exibition_id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Offer { get; set; }
        public virtual Exhibition Exhibition { get; set; }
        public virtual User User { get; set; }

    }
}
