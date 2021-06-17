using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("Exhibition")]
    public partial class Exhibition
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string OraganizerName { get; set; }

        public string Description { get; set; }
        [ForeignKey("User")]
        public int Organizer_User_Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public virtual User User { get; set; }

    }
}
