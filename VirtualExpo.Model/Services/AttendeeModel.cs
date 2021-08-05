using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Model.Services
{
    public class AttendeeModel:User
    {
        public int WorkId { get; set; }
        public int EduId { get; set; }
        public string DegreeName { get; set; }
        public string Institute { get; set; }

        public DateTime PassingYear { get; set; }

        public string Grade { get; set; }

        public string EmployeerName { get; set; }

        public string Designation { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string IndustryName { get; set; }
        public string WorkingStatusStr { get; set; }
        public string Location { get; set; }
        public string GenderTypename { get; set; }
    }
}
