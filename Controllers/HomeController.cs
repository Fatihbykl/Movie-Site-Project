using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Data;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Movie> movies = new List<Movie>();
            var randomMovies = _context.Movies.OrderBy(r => Guid.NewGuid()).Take(6).ToList();
            var lastReviews = _context.UserMovies
                                .Where(s => s.Review != null)
                                .Select(s => new
                                {
                                    Rating = s.Rating,
                                    Review = s.Review,
                                    Date = s.Date,
                                    Title = s.movie.Title,
                                    ImageURL = s.movie.ImageURL,
                                    Year = s.movie.Year,
                                    MovieID = s.movie.MovieID,
                                    UserName = s.user.UserName,
                                })
                                .Distinct()
                                .OrderByDescending(s => s.Date)
                                .Take(4);
            List<UserMovieQuery> result = new List<UserMovieQuery>();
            foreach (var item in lastReviews)
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
                    UserName = item.UserName,
                };
                result.Add(obj);
            }

            var lastActivities = _context.UserMovies
                                .Select(s => new
                                {
                                    Rating = s.Rating,
                                    Review = s.Review,
                                    Date = s.Date,
                                    Title = s.movie.Title,
                                    ImageURL = s.movie.ImageURL,
                                    Year = s.movie.Year,
                                    MovieID = s.movie.MovieID,
                                    UserName = s.user.UserName,
                                })
                                .Distinct()
                                .OrderByDescending(s => s.Date)
                                .Take(6);
            List<UserMovieQuery> resultActivities = new List<UserMovieQuery>();
            foreach (var item in lastActivities)
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
                    UserName = item.UserName,
                };
                resultActivities.Add(obj);
            }

            dynamic model = new System.Dynamic.ExpandoObject();
            model.randomMovies = randomMovies;
            model.lastReviews = result;
            model.lastActivities = resultActivities;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult SearchMovie(string movie)
        {
            var result = _context.Movies.Where(x => x.Title.Contains(movie)).ToList();
            return Json(result);
        }
    }
}
