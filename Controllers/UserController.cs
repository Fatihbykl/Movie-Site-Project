using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProgramlamaProje.Models;
using Microsoft.AspNetCore.Http;
using WebProgramlamaProje.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


namespace WebProgramlamaProje.Controllers
{
    
    public class UserController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signinManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<User> context, SignInManager<User> signin, ILogger<UserController> logger)
        {
            userManager = context;
            signinManager = signin;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Signup(RegisterModel model)
        {
            if (User.Identity.IsAuthenticated) { return Redirect("../Home/Index"); }

            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Signin");
                }
                else
                {
                    foreach (var i in result.Errors)
                    {
                        ModelState.AddModelError("", i.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            if (User.Identity.IsAuthenticated) { return Redirect("../Home/Index"); }
            return View();
        }
        [HttpGet]
        public IActionResult Signin()
        {
            if (User.Identity.IsAuthenticated) { return Redirect("../Home/Index"); }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signin(LoginModel model)
        {
            if (User.Identity.IsAuthenticated) { return Redirect("../Home/Index"); }

            Console.WriteLine("asdsadsad - " + ModelState.IsValid);
            if (ModelState.IsValid)
            {
                var result = await signinManager.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure: false);
                Console.WriteLine("result  " + result);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Redirect("../Home/Index");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signinManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Redirect("../Home/Index");//Homepage git.
        }
    }
}
