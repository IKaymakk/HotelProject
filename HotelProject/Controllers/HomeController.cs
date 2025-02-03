using BusinessLayer.Services;
using EntityLayer.Entities;
using HotelProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Room()
        {
            return View();
        }
        public IActionResult RoomDetails()
        {
            return View();
        }
        public IActionResult Activitys()
        {
            return View();
        }
        public IActionResult ActivityDetail()
        {
            return View();
        }
        public IActionResult Contact()
        {
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

    }
}
