using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("WorkExperience")]
    public partial class WorkExperience
    {
        [Key]
        public int Id { get; set; }

        public string EmployeerName { get; set; }

        public string Designation { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string IndustryName { get; set; }
        public int WorkingStatus { get; set; }
        public string Location { get; set; }
        [ForeignKey("User")]
        public int Attendee_User_Id { get; set; }
        public virtual User User { get; set; }
    }
}
