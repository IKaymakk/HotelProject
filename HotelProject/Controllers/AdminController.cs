using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult RoomSet()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        public ActionResult frmAddFeature()
        {
            return View();
        }
        public ActionResult frmAddPicture()
        {
            return View();
        }
    }
}
