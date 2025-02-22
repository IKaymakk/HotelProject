using DataAccessLayer;
using DataAccessLayer.DataAccessLayer;
using EntityLayer.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class HomeService
    {
        private readonly ILogger<HomeService> _logger;
        private readonly ApplicationDbContext _context;

        public HomeService(ILogger<HomeService> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;

        }

        public List<Room> GetMainPageRooms()
        {
            try
            {
                var popularRooms = _context.Rooms
                    .Where(x => x.isMainPage == true)
                    .ToList(); 

                foreach (var room in popularRooms)
                {
                    var roomImages = _context.RoomImages
                        .Where(x => x.RoomId == room.Id && !x.isDeleted) 
                        .ToList();

                    if (roomImages.Any())
                    {
                        var firstImage = roomImages.FirstOrDefault();
                        if (firstImage?.FileData != null)
                        {
                            room.ImageBase64 = Convert.ToBase64String(firstImage.FileData);
                        }
                    }
                }

                return popularRooms;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting room images: ", ex);
                return null;
            }
        }   
      


    }
}
