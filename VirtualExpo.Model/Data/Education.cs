using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("Education")]
    public partial class Education
    {
        [Key]
        public int Id { get; set; }

        public string DegreeName { get; set; }

        public string Institute { get; set; }

        public DateTime PassingYear { get; set; }

        public string Grade { get; set; }
        [ForeignKey("User")]
        public int Attendee_User_Id { get; set; }
        public virtual User User { get; set; }

    }
}
