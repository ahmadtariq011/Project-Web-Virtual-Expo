using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("SocialNetwork")]
    public class SocialNetwork
    {
        [Key]
        public int Id { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string SnapChat { get; set; }
        public string Linkdin { get; set; }
        public string Website { get; set; }
        [ForeignKey("ExhibitorDescription")]
        public int Exhibitor_Id { get; set; }
        public virtual ExhibitorDescription Exhibitor { get; set; }
    }
}
