using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Model.Services
{
    public class RequestOrganizerModel:RequestOrganizer
    {
        public IFormFile Image { get; set; }
    }
}
