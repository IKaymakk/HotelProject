using DataAccessLayer.DataAccessLayer;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


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
        //public List<RoomCategory> GetRoomCategories()
        //{
        //    try
        //    {

        //        return categories;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error getting categories: ", ex);
        //        return null;
        //    }
        //}
        public List<Room> GetMainPageRooms()
        {
            try
            {
                List<Room> popularRooms = new List<Room>();
                var categories = _context.RoomCategories
                    .Include(c => c.Rooms.Where(r => !r.isDeleted))
                        .ThenInclude(r => r.RoomImages.Where(img => !img.isDeleted))
                    .Include(c => c.Rooms.Where(r => !r.isDeleted))
                        .ThenInclude(r => r.RoomFeatures.Where(f => !f.isDeleted))
                    .ToList();

                foreach (var category in categories)
                {
                    var roomsInCategory = category.Rooms.Where(x => x.isMainPage == true && x.isDeleted == false).ToList();
                    popularRooms.AddRange(roomsInCategory);  
                }

                return popularRooms;
            }
            catch (Exception ex)
            {
                return new List<Room>();  
            }

         
        }
        public ContactInformations GetContactInformations()
        {
            try
            {
                var contactInformations = _context.ContactInformations.First();
                return contactInformations;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting contact infos: ", ex);
                return null;
            }
        }

        public AboutUs GetAbout()
        {
            try
            {
                var about = _context.AboutUs.First();
                return about;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting contact infos: ", ex);
                return null;
            }
        }



    }
}
