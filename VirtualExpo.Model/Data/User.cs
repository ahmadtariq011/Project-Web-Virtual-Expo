using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VirtualExpo.Model.Data
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public string Telephone { get; set; }

        public int UserType { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; } 
        [Required]
        public string CNIC { get; set; }

        public int GenderType { get; set; }
        public DateTime LogTime { get; set; }


    }
}
