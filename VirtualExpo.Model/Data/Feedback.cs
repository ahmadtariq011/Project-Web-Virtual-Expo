using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExpo.Model.Data
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [ForeignKey("Exhibition")]
        public int ExhibitionId { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsRead { get; set; } = false;
    }
}
