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
    public class HomeController(ILogger<HomeController> logger, RoomService roomService, HomeService homeService, EmailService mailService) : BaseController(roomService, homeService)
    {
        public IActionResult Index()
        {
            try
            {

                return View();
            }

            catch (Exception ex)
            {
                logger.LogError("Error getting rooms: ", ex);
                return View();
            }


        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Room()
        {
            var rooms = roomService.GetCategoriesWithRooms();
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
            RoomDTO room = roomService.GetRoomDetail(id);
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
            var result = await mailService.SendMailAsync(model);
            return Json(new { success = result });
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
