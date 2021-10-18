using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Model.Services
{
    public class ExhibitorDescriptionModel:UserModel
    {
        public string Name { get; set; }
        public string Offer { get; set; }
        public string Moto { get; set; }
        public int ExhibitionId { get; set; }

        public int UserId { get; set; }
    }
}
