﻿using BusinessLayer.Services;
using EntityLayer.DTO;
using EntityLayer.Entities;
using EntityLayer.ViewModel;
using HotelProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Pkcs;

namespace HotelProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoomService _roomService;
        private readonly HomeService _homeService;

        public AdminController(ILogger<AdminController> logger, RoomService roomService, HomeService homeService)
        {
            _logger = logger;
            _roomService = roomService;
            _homeService = homeService;
        }

        public IActionResult SetSocialMedia()
        {
            return View();
        }
        public IActionResult frmEditPicture()
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
                    return View(new List<RoomDTO>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting rooms: ", ex);
                return View(new List<RoomDTO>());
            }
        }


        public IActionResult ResetPassword()
        {
            var result = _homeService.GetAbout();
            var result2 = _homeService.GetContactInformations();
            var result3 = _homeService.GetSocialMediaLinks();
            var model = new SettingsViewModel
            {
                AboutUs = result,
                ContactInformations = result2,
                SocialMediaLinks = result3,
            };
            return View(model);
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
        public JsonResult InsertFeatures([FromBody] RoomFeatures model)
        {
            try
            {
                var result = _roomService.InsertFeatures(model);
                if (result == true)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inserting or updating features: ", ex);
                return Json(new { success = false, message = "Error inserting or updating features", error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteFeatures(List<int> FeatureIds)
        {
            try
            {
                var results = new List<bool>();

                foreach (var FeatureId in FeatureIds)
                {
                    var result = _roomService.DeleteFeatures(FeatureId);
                    results.Add(result);
                }

                if (results.Count != 0 && results.All(r => r))
                {
                    return Json(new { success = true, message = "Features deleted successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Some features could not be deleted" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting features: ", ex);
                return Json(new { success = false, message = "Error deleting features", error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateFeatures(List<RoomFeatureDTO> Features)
        {
            try
            {
                var results = new List<bool>();

                foreach (var Feature in Features)
                {
                    var result = _roomService.UpdateFeatures(Feature);
                    results.Add(result);
                }

                if (results.Count != 0 && results.All(r => r))
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating features: ", ex);
                return Json(new { success = false, message = "Error deleting features", error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteImage(int ImageId)
        {
            try
            {
                var result = _roomService.DeleteImage(ImageId);
                if (result == "Success")
                {
                    return Json(new { success = true, data = result });
                }
                else
                {
                    return Json(new { success = false, data = result });
                }
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
                            FileData = memoryStream.ToArray(),
                            isCoverImage = false,
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

        public ActionResult frmEditFeature()
        {
            return View();
        }
        public ActionResult frmEditPrice()
        {
            return View();
        }
        public ActionResult UpcomingRez()
        {
            return View();
        }
        public ActionResult PastRez()
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



        [HttpGet]
        public JsonResult GetRoomDetail(int roomId)
        {
            try
            {
                var result = _roomService.GetRoomDetail(roomId);

                if (result != null)
                {
                    return Json(new { success = true, data = result });
                }

                return Json(new { success = false, message = "Room not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting room details: ", ex);
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateExtraSettings(Room model)
        {
            try
            {
                var result = _roomService.UpdateExtraSettings(model);

                if (result != false)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting room details: ", ex);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult updateContactSettings(ContactInformations model)
        {
            try
            {
                var result = _roomService.updateContactSettings(model);

                if (result != false)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting room details: ", ex);
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult updateAboutUsSettings(AboutUs model)
        {
            try
            {
                var result = _roomService.updateAboutUsSettings(model);

                if (result != false)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting room details: ", ex);
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult SetCoverImage(int ImageId, int RoomId)
        {
            try
            {
                var result = _roomService.SetCoverImage(ImageId, RoomId);

                if (result != false)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting room details: ", ex);
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteRoom(int RoomId)
        {
            try
            {
                var result = _roomService.DeleteRoom(RoomId);

                if (result != false)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting room details: ", ex);
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateSocialMediaLinks(SocialMediaLinks model)
        {
            try
            {
                var result = _roomService.UpdateSocialMediaLinks(model);

                if (result != false)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting room details: ", ex);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetRooms()
        {
            var result = _roomService.GetRooms();
            return Json(result);
        }


    }
}
