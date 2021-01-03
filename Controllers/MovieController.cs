using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Data;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public MovieController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("movie/{movieID}")]
        public IActionResult Index(int movieID)
        {
            var result = _context.Movies.Where(x => x.MovieID == movieID).FirstOrDefault();
            return View(result);
        }
        [HttpGet]
        [Route("movie-log/{movieID}")]
        public IActionResult LogMovie(int movieID, string rating, string review)
        {
            User _user = _userManager.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            Movie _movie = _context.Movies.Where(x => x.MovieID == movieID).FirstOrDefault();
            UserMovie log = new UserMovie
            {
                user = _user,
                movie = _movie,
                Date = DateTime.Now,
                Rating = int.Parse(rating),
                Review = review,
            };
            _context.UserMovies.Add(log);
            _context.SaveChanges();
            return Redirect("../Home/Index");
        }
    }
}
