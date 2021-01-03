using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class UserMovieQuery
    {
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public int Year { get; set; }
        public int MovieID { get; set; }
        public string UserName { get; set; }
    }
}
