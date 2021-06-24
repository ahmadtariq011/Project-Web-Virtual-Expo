using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("UserExhibitionJunction")]
    public class AttendeeExhibitionJunction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Exhibition")]
        public int Exibition_id { get; set; }
        [ForeignKey("User")]
        public int Attendee_Id { get; set; }
    }
}
