using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(25, ErrorMessage = "En fazla 25 harf girilebilir.")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30, ErrorMessage = "En fazla 30 harf girilebilir.")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
