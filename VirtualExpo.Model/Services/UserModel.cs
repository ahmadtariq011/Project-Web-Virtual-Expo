using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Model.Services
{
    public class UserModel:User
    {
        public string UserTypeName { get; set; }
        public string GenderTypename { get; set; }
        public string UserStatusStr { get; set; }
        public IFormFile Image { get; set; }

    }
}
