using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Data;

namespace WebProgramlamaProje.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("movie/{movieID}")]
        public IActionResult Index(int movieID)
        {
            var result = _context.Movies.Where(x => x.MovieID == movieID).FirstOrDefault();
            return View(result);
        }
    }
}
