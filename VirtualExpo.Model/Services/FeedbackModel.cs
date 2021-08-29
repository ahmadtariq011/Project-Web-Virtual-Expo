using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Model.Services
{
    public class FeedbackModel:Feedback
    {
        public string CreatedDateStr { get; set; }
        public string ExhibitionName { get; set; }
    }
}
