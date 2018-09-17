using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sistem_Ventas.Models;

namespace Sistem_Ventas.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IServiceProvider serviceProvider)
        {
            //CreateRoles(serviceProvider);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            string mensaje;
            try
            {
                var roleMananger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string[] rolesName = { "Admin", "User" };
                foreach (var item in rolesName)
                {
                    var roleExist = await roleMananger.RoleExistsAsync(item);
                    if (!roleExist)
                    {
                        await roleMananger.CreateAsync(new IdentityRole(item));
                    }
                }
                var user = await userManager.FindByIdAsync("590586a7-f286-4539-ad1b-1e1dbb0cebe2");
                await userManager.AddToRoleAsync(user, "Admin");
            }
            catch (Exception ex)
            {

                mensaje = ex.Message;
            }
            
        }

    }
}
