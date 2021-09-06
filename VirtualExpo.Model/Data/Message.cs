using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string ExhibitionIdentifier { get; set; }
        public string MessageText { get; set; }
        public string SenderName { get; set; }

    }
}
