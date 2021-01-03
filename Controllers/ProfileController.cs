using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Data;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<User> userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<User> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            _context = context;
        }

        [Route("{username}")]
        public IActionResult Index(string username)
        {
            User user = userManager.Users.Where(x => x.UserName == username).FirstOrDefault();
            return View(user);
        }
        [Route("{username}/edit-profile")]
        public IActionResult EditProfile(string username)
        {
            if(User.Identity.Name != username) { return Forbid(); }
            User user = userManager.Users.Where(x => x.UserName == username).FirstOrDefault();

            return View(user);
        }
        [Route("{username}/films")]
        public IActionResult Films(string username)
        {
            User user = userManager.Users.Where(x => x.UserName == username).FirstOrDefault();
            ViewBag.username = user.Name;
            var movies = _context.UserMovies
                                .Where(s => s.user == user)
                                .Select(s => new
                                {
                                    Rating = s.Rating,
                                    Review = s.Review,
                                    Date = s.Date,
                                    Title = s.movie.Title,
                                    ImageURL = s.movie.ImageURL,
                                    Year = s.movie.Year,
                                    MovieID = s.movie.MovieID,
                                })
                                .Distinct()
                                .OrderByDescending(s => s.Date);
            List<UserMovieQuery> result = new List<UserMovieQuery>();
            foreach (var item in movies)
            {
                var obj = new UserMovieQuery()
                {
                    Rating = item.Rating,
                    Review = item.Review,
                    Date = item.Date,
                    Title = item.Title,
                    ImageURL = item.ImageURL,
                    MovieID = item.MovieID,
                    Year = item.Year,
                };
                result.Add(obj);
            }
            return View(result);
        }

        [Route("{username}/reviews")]
        public IActionResult Reviews(string username)
        {
            User user = userManager.Users.Where(x => x.UserName == username).FirstOrDefault();
            return View(user);
        }
    }
}
