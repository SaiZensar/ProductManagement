using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagement.Data;
using ProductManagement.Global;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductData _data;
        private readonly ApplicationDbContext _adb;
        public HomeController(ILogger<HomeController> logger, IProductData data, ApplicationDbContext adb)
        {
            _logger = logger;
            _data = data;
            _adb = adb;
        }

        public IActionResult Index()
        {
            GlobalVariables.cartItemCount = 0;
            if(User.Identity.IsAuthenticated)
            {
                GlobalVariables.cartItemCount = _adb.ShoppingCarts.Where(user => user.ApplicationUserId == User.Identity.Name).ToList().Count;
            }
            var model = _data.GetAll();

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
    }
}
