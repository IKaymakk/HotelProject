using BusinessLayer.Services;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult SetSocialMedia()
        {
            return View();
        }

        public IActionResult SetPopularRoom()
        {
            return View();
        }
        public IActionResult SetContact()
        {
            return View();
        }
        public IActionResult SetAboutUs()
        {
            return View();
        }
        public IActionResult RoomSet()
        {
            try
            {
                var result = _roomService.GetRooms();

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

        public IActionResult ResetPassword()
        {
            return View();
        }





        [HttpPost]
        public JsonResult InsertRoom([FromBody] Room model)
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
        public JsonResult UpdateRoom([FromBody] Room model)
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
        public JsonResult InsertOrUpdateFeatures([FromBody] RoomFeatures model)
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
        public IActionResult InsertRoomImages([FromForm] List<IFormFile> imageFiles, [FromForm] int roomId)
        {
            try
            {
                foreach (var imageFile in imageFiles)
                {

                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);


                        var roomImage = new RoomImages
                        {
                            RoomId = roomId,
                            FileName = imageFile.FileName,
                            FileType = imageFile.ContentType,
                            FileData = memoryStream.ToArray()
                        };

                        _roomService.InsertRoomImages(roomImage);
                    }
                }

                return Ok(new { success = true, message = "Images inserted successfully" });
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
        public ActionResult frmAddFeature()
        {
            return View();
        }
        public ActionResult frmAddPicture()
        {
            return View();
        }

        public IActionResult frmAddRoom()
        {
            try
            {
                var result = _roomService.GetRoomCategories();

                if (result != null)
                {
                    return View(result);
                    ViewBag.Categories = result;
                }
                else
                {
                    return View("Error", "No rooms available");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting rooms: ", ex);
                return View("Error", ex.Message);
            }
        }

    }
}
