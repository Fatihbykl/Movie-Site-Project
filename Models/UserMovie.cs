using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Data;

namespace WebProgramlamaProje.Models
{
    public class UserMovie
    {
        [Key]
        public int id { get; set; }

        public virtual User user { get; set; }
        public virtual Movie movie { get; set; }

        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }


    }
}
