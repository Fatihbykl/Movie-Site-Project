using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Data;

namespace WebProgramlamaProje.Models
{
    public class DbQueryFunc
    {
        private readonly ApplicationDbContext _context;

        public DbQueryFunc(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserMovieQuery> GetInfoWhereUser(User user)
        {
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
                                    UserName = s.user.UserName,
                                })
                                .Distinct()
                                .OrderByDescending(s => s.Date);
            return AnonymToUMQuery(movies);            
        }

        public List<UserMovieQuery> GetInfoWhereUserReviewNotNull(User user)
        {
            var reviews = _context.UserMovies
                                .Where(s => s.user == user && !String.IsNullOrWhiteSpace(s.Review))
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
                                .OrderByDescending(s => s.Date);
            return AnonymToUMQuery(reviews);
        }

        public List<UserMovieQuery> GetLast4Reviews()
        {
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
            return AnonymToUMQuery(lastReviews);
        }

        public List<UserMovieQuery> GetLast6Activities()
        {
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
            return AnonymToUMQuery(lastActivities);
        }

        private List<UserMovieQuery> AnonymToUMQuery(dynamic query)
        {
            List<UserMovieQuery> result = new List<UserMovieQuery>();
            foreach (var item in query)
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
            return result;
        }
    }
}
