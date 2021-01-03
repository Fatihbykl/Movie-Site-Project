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
        private DbQueryFunc funcs;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            funcs = new DbQueryFunc(_context);
        }

        public IActionResult Index()
        {
            List<Movie> movies = new List<Movie>();
            var randomMovies = _context.Movies.OrderBy(r => Guid.NewGuid()).Take(6).ToList();
            var lastReviews = funcs.GetLast4Reviews();
            var lastActivities = funcs.GetLast6Activities();

            dynamic model = new System.Dynamic.ExpandoObject();
            model.randomMovies = randomMovies;
            model.lastReviews = lastReviews;
            model.lastActivities = lastActivities;
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
