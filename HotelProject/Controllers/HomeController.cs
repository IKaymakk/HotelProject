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
        private readonly HomeService _homeService;
        private readonly RoomService _roomService;

        public HomeController(ILogger<HomeController> logger, HomeService homeService, RoomService roomService)
        {
            _logger = logger;
            _homeService = homeService;
            _roomService = roomService;
        }

 
        public IActionResult Index()
        {
            try
            {
                var result = _homeService.GetMainPageRooms();
                var result2 = _roomService.GetRoomCategories();

                if (result2 != null)
                {
                    ViewBag.Categories = result2;
                }
                if (result != null)
                {
                    return View(result);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting rooms: ", ex);
                return View();
            }

            
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
