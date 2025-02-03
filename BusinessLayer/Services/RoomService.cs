using DataAccessLayer;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class RoomService
    {
        private readonly ILogger<RoomService> _logger;
        private readonly ApplicationDbContext _context;

        public RoomService(ILogger<RoomService> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;

        }

        public string InsertRoom(Room model)
        {
            try
            {
                _context.Rooms.Add(model);

                _context.SaveChanges();

                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inserting room: ", ex);

                return "Error: " + ex.Message;
            }
        }


        public string UpdateRoom(Room model)
        {
            try
            {
                var existingRoom = _context.Rooms.FirstOrDefault(r => r.Id == model.Id);
                if (existingRoom == null)
                {
                    return "Error: Room not found.";
                }

                existingRoom.Name = model.Name;
                existingRoom.Price = model.Price;


                _context.Rooms.Update(existingRoom);

                _context.SaveChanges();

                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating room: ", ex);

                return "Error: " + ex.Message;
            }
        }

        public string InsertOrUpdateFeatures(RoomFeatures model)
        {
            try
            {
                var existRecord = _context.RoomFeatures.FirstOrDefault(x => x.RoomId == model.RoomId);

                if (existRecord != null)
                {
                    existRecord.Features = model.Features;
                    _context.RoomFeatures.Update(existRecord);
                }
                else
                {
                    _context.RoomFeatures.Add(model);
                }

                _context.SaveChanges();

                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Inserting Or Updating features : ", ex);
                return "Error: " + ex.Message;
            }
        }
        public string DeleteFeatures(int roomId)
        {
            try
            {
                var feature = _context.RoomFeatures.FirstOrDefault(x => x.RoomId == roomId);

                if (feature != null)
                {
                    feature.isDeleted = true;
                    _context.RoomFeatures.Update(feature);
                    _context.SaveChanges();

                    return "Success";
                }

                return "Feature not found";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting features: ", ex);
                return "Error: " + ex.Message;
            }
        }

        public string InsertRoomImages(List<IFormFile> imageFiles, int roomId)
        {
            try
            {
                foreach (var imageFile in imageFiles)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var roomImage = new RoomImages
                        {
                            RoomId = roomId,
                            FileName = imageFile.FileName,
                            FileType = imageFile.ContentType
                        };

                        using (var memoryStream = new MemoryStream())
                        {
                            imageFile.CopyTo(memoryStream);
                            roomImage.FileData = memoryStream.ToArray();
                        }

                        _context.RoomImages.Add(roomImage);
                    }
                }

                _context.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inserting room images: ", ex);
                return "Error: " + ex.Message;
            }
        }

        public string DeleteRoomImages(int ImageId)
        {
            try
            {
                var image = _context.RoomImages.FirstOrDefault(x => x.ImageId == ImageId);

                if (image != null)
                {
                    image.isDeleted = true;
                    _context.RoomImages.Update(image);
                    _context.SaveChanges();

                    return "Success";
                }

                return "Room image not found";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting room image: ", ex);
                return "Error: " + ex.Message;
            }
        }


    }
}
