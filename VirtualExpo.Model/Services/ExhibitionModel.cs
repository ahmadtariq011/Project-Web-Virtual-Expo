using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Model.Services
{
    public class ExhibitionModel:Exhibition
    {
        public string StartdateSrt { get; set; }
        public string EndDateStr { get; set; }
        public string CreatedDateStr { get; set; }
        public string ExhibitionStatusStr { get; set; }

    }
}
