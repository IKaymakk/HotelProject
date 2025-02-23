using BusinessLayer.Services;
using EntityLayer.DTO;
using EntityLayer.Entities;
using HotelProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace HotelProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;
        private readonly RoomService _roomService;
        private readonly EmailService _mailService;

        public HomeController(ILogger<HomeController> logger, HomeService homeService, RoomService roomService, EmailService mailService)
        {
            _logger = logger;
            _homeService = homeService;
            _roomService = roomService;
            _mailService = mailService;
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
            var rooms = _roomService.GetCategoriesWithRooms();
            return View(rooms);
        }
        public IActionResult RoomDetails()
        {
            return View();
        }
        public IActionResult Activitys()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            RoomDTO room = _roomService.GetRoomDetail(id);
            return View(room);
        }
        public IActionResult Detail301()
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
        public IActionResult CaveSuitRoom()
        {
            return View();
        }
        public IActionResult DeluxeRoom()
        {
            return View();
        }
        public IActionResult DoubleArchRoom()
        {
            return View();
        }
        public IActionResult KingSuitRoom()
        {
            return View();
        }
        public IActionResult StandartArchRoom()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SendMail(MailModel model)
        {
            var result = await _mailService.SendMailAsync(model);
            return Json(new { success = result });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
