using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebProgramlamaProje.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int Year { get; set; }
        public int Runtime { get; set; }
        public string Genres { get; set; }
        public float ImdbRating { get; set; }
        public string Director { get; set; }
        public virtual ICollection<User> Viewers { get; set; }
    }
}
