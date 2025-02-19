using DataAccessLayer;
using EntityLayer.Entities;
using Microsoft.Extensions.Logging;

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

        public void InsertRoomImages(RoomImages image)
        {
            _context.RoomImages.Add(image);
            _context.SaveChanges();
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

        public List<Room> GetRooms()
        {
            try
            {
                var rooms = _context.Rooms.ToList();
                return rooms;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting room: ", ex);
                return null;
            }
        }
        public List<RoomCategory> GetRoomCategories()
        {
            try
            {
                var categories = _context.RoomCategories.ToList();
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting categories: ", ex);
                return null;
            }
        }


    }
}
