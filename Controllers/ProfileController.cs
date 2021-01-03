using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Data;
using WebProgramlamaProje.Models;
using Microsoft.Extensions.Localization;

namespace WebProgramlamaProje.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<User> userManager;
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<ProfileController> _localizer;
        private DbQueryFunc funcs;

        public ProfileController(UserManager<User> userManager, ApplicationDbContext context, IStringLocalizer<ProfileController> loc)
        {
            this.userManager = userManager;
            _context = context;
            _localizer = loc;
            funcs = new DbQueryFunc(_context);
        }

        [Route("{username}")]
        public IActionResult Index(string username)
        {
            User user = userManager.Users.Where(x => x.UserName == username).FirstOrDefault();
            ViewBag.username = user.UserName;
            var result = funcs.GetInfoWhereUser(user).Take(4);
            var reviews = funcs.GetInfoWhereUserReviewNotNull(user).Take(2);
            dynamic model = new System.Dynamic.ExpandoObject();
            model.act = result;
            model.rev = reviews;
            return View(model);
        }

        [Route("{username}/films")]
        public IActionResult Films(string username)
        {
            User user = userManager.Users.Where(x => x.UserName == username).FirstOrDefault();
            ViewBag.username = user.UserName;
            var result = funcs.GetInfoWhereUser(user);
            return View(result);
        }

        [Route("{username}/reviews")]
        public IActionResult Reviews(string username)
        {
            User user = userManager.Users.Where(x => x.UserName == username).FirstOrDefault();
            ViewBag.username = user.UserName;
            var result = funcs.GetInfoWhereUserReviewNotNull(user);
            return View(result);
        }
    }
}
