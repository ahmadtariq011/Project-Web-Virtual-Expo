using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Model.Services
{
    public class MediaLinkModel:MediaLinks
    {
        public IFormFile PictureFile { get; set; }
        public IFormFile VideoFile { get; set; }
        public IFormFile PdfFile { get; set; }

    }
}
