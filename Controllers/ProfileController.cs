﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Models;

namespace WebProgramlamaProje.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<User> userManager;

        public ProfileController(UserManager<User> userManager)
        {
            this.userManager = userManager;
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
            if(User.Identity.Name != username)
            {
                return Forbid();
            }
            return View();
        }
    }
}