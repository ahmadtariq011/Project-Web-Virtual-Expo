using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("Session")]
    public partial class Session
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ExhibitorDescription")]
        public int ExhibitorId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
        public int SessionStatus { get; set; }//active or not
        public string Description { get; set; }
        public int Status { get; set; }
        public virtual ExhibitorDescription Exhibitor { get; set; }
    }
}
