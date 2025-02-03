using BusinessLayer.Services;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoomService _roomService;

        public AdminController(ILogger<AdminController> logger, RoomService roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }
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





        [HttpPost]
        public JsonResult InsertRoom(Room model)
        {
            try
            {
                var insertRoom = _roomService.InsertRoom(model);
                return Json(new { success = true, message = "Room inserted successfully", data = insertRoom });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inserting room: ", ex);
                return Json(new { success = false, message = "Error inserting room", error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateRoom(Room model)
        {
            try
            {
                var updateRoom = _roomService.UpdateRoom(model);
                return Json(new { success = true, message = "Room updated successfully", data = updateRoom });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating room: ", ex);
                return Json(new { success = false, message = "Error updating room", error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult InsertOrUpdateFeatures(RoomFeatures model)
        {
            try
            {
                var result = _roomService.InsertOrUpdateFeatures(model);
                return Json(new { success = true, message = "Feature inserted or updated successfully", data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inserting or updating features: ", ex);
                return Json(new { success = false, message = "Error inserting or updating features", error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteFeatures(int RoomId)
        {
            try
            {
                var result = _roomService.DeleteFeatures(RoomId);
                return Json(new { success = true, message = "Features deleted successfully", data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting features: ", ex);
                return Json(new { success = false, message = "Error deleting features", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("InsertRoomImages")]
        public IActionResult InsertRoomImages([FromForm] List<IFormFile> imageFiles, [FromForm] int roomId)
        {
            try
            {
                var result = _roomService.InsertRoomImages(imageFiles, roomId);
                if (result == "Success")
                {
                    return Ok(new { success = true, message = "Images inserted successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = result });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inserting room images: ", ex);
                return StatusCode(500, new { success = false, message = "An error occurred while processing the images", error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteRoomImages(int ImageId)
        {
            try
            {

                var result = _roomService.DeleteRoomImages(ImageId);
                return Json(new { success = true, data = result });


            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting room images: ", ex);
                return Json(new { success = false, error = ex.Message });

            }
        }
    }
}
