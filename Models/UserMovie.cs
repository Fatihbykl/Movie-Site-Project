using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class UserMovie
    {    
        public int IDUser { get; set; }       
        public int IDMovie { get; set; }

        public virtual User user { get; set; }
        public virtual Movie Movie { get; set; }

        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}
