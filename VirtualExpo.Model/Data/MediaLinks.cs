using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("MediaLinks")]
    public partial class MediaLinks
    {
        [Key]
        public int Id { get; set; }

        public string Picture { get; set; }

        public string PictureDescription { get; set; }

        public string Video { get; set; }
        public string VideoDescription { get; set; }
        public string Link { get; set; }
        public string LinkDescription { get; set; }
        [ForeignKey("ExhibitorDescription")]
        public int Exhibitor_Id { get; set; }
        public virtual ExhibitorDescription Exhibitor { get; set; }

    }
}
