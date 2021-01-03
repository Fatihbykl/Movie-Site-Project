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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<User> userManager;

        public AdminController(ApplicationDbContext context, UserManager<User> um)
        {
            _context = context;
            userManager = um;
        }
        [Route("adminpanel")]
        public async Task<IActionResult> IndexAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (!await userManager.IsInRoleAsync(user, "admin"))
            {
                return Forbid();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (!await userManager.IsInRoleAsync(user, "admin"))
            {
                return Forbid();
            }
            _context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName,
            });
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(string roleName, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if(!await userManager.IsInRoleAsync(user, roleName) && user != null)
            {
                var r = await userManager.AddToRoleAsync(user, roleName);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
