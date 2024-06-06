using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Context;

namespace ServiceHub.WebApp.Areas.SystemAdmin.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly DataContext _dataContext;

        public UsersController(RoleManager<IdentityRole> roleManager,
                        SignInManager<IdentityUser> signInManager,
                        UserManager<IdentityUser> userManager,
                        DataContext dataContext)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var usera = await _dataContext.Users.ToListAsync();
            return View(usera);
        }
    }
}