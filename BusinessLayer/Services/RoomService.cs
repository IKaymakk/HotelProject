﻿using AutoMapper;
using AutoMapper.Features;
using DataAccessLayer;
using DataAccessLayer.DataAccessLayer;
using EntityLayer.DTO;
using EntityLayer.Entities;
using HotelProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
    public class RoomService
    {
        private readonly ILogger<RoomService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public RoomService(ILogger<RoomService> logger, ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;

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

        public bool InsertFeatures(RoomFeatures model)
        {
            try
            {
                _context.RoomFeatures.Add(model);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Inserting Or Updating features : ", ex);
                return false;
            }
        }
        public bool DeleteFeatures(int FeatureId)
        {
            try
            {
                var feature = _context.RoomFeatures.FirstOrDefault(x => x.FeatureId == FeatureId);

                if (feature != null)
                {
                    feature.isDeleted = true;
                    _context.RoomFeatures.Update(feature);
                    _context.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting features: ", ex);
                return false;
            }
        }
        public bool UpdateFeatures(RoomFeatureDTO dto)
        {
            try
            {
                var feature = _context.RoomFeatures.FirstOrDefault(x => x.FeatureId == dto.FeatureId);

                if (feature != null)
                {
                    feature.Features = dto.Features;
                    _context.RoomFeatures.Update(feature);
                    _context.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting features: ", ex);
                return false;
            }
        }
        public string DeleteImage(int ImageId)
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

                return "Image not found";
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



        public List<RoomCategoryDTO> GetCategoriesWithRooms()
        {
            var categories = _context.RoomCategories
                .Include(c => c.Rooms.Where(r => !r.isDeleted))
                    .ThenInclude(r => r.RoomImages.Where(img => !img.isDeleted))
                .Include(c => c.Rooms.Where(r => !r.isDeleted))
                    .ThenInclude(r => r.RoomFeatures.Where(f => !f.isDeleted))
                .ToList();

            var categoriesDto = _mapper.Map<List<RoomCategoryDTO>>(categories);
            return categoriesDto;
        }

        public List<RoomDTO> GetRooms()
        {
            try
            {
                var rooms = _context.Rooms
                    .Where(x => !x.isDeleted)
                    .Include(x => x.RoomImages.Where(img => !img.isDeleted))
                    .Include(x => x.RoomFeatures.Where(f => !f.isDeleted))
                    .ToList();

                var roomDTOs = _mapper.Map<List<RoomDTO>>(rooms);

                return roomDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting rooms: ", ex);
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

        public RoomDTO GetRoomDetail(int roomId)
        {
            try
            {
                var room = _context.Rooms
                  .Where(x => !x.isDeleted)
                  .Include(x => x.RoomImages.Where(img => !img.isDeleted))
                  .Include(x => x.RoomFeatures.Where(f => !f.isDeleted))
                  .FirstOrDefault(x => x.Id == roomId);


                if (room == null)
                    return null;

                var roomDTO = _mapper.Map<RoomDTO>(room);

                return roomDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting room details", ex);
            }
        }

        public bool UpdateExtraSettings(Room model)
        {
            try
            {
                var room = _context.Rooms.FirstOrDefault(r => r.Id == model.Id);
                if (room == null)
                    return false;
                room.Price = model.Price;
                room.Description = model.Description;
                room.Capacity = model.Capacity;
                room.isMainPage = model.isMainPage;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating room details", ex);
            }
        }
        public bool updateContactSettings(ContactInformations model)
        {
            try
            {
                var contact = _context.ContactInformations.FirstOrDefault();
                if (contact == null)
                    return false;
                contact.Email = model.Email;
                contact.Phone = model.Phone;
                contact.Address = model.Address;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating contact details", ex);
            }
        }
        public bool updateAboutUsSettings(AboutUs model)
        {
            try
            {
                var about = _context.AboutUs.FirstOrDefault();
                if (about == null)
                    return false;
                about.AboutUsHeader = model.AboutUsHeader;
                about.AboutUsText = model.AboutUsText;
                about.AboutUsHeader2 = model.AboutUsHeader2;
                about.AboutUsText2 = model.AboutUsText2;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating about details", ex);
            }
        }
        public bool SetCoverImage(int ImageId, int RoomId)
        {
            try
            {
                var image = _context.RoomImages.FirstOrDefault(x => x.ImageId == ImageId && x.RoomId == RoomId);
                if (image == null)
                {
                    return false;
                }

                var existingCover = _context.RoomImages.Where(x => x.RoomId == RoomId && x.isCoverImage).ToList();
                foreach (var img in existingCover)
                {
                    img.isCoverImage = false;
                }

                image.isCoverImage = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Kapak resmi güncellenirken hata oluştu", ex);
            }
        }
        public bool DeleteRoom(int RoomId)
        {
            try
            {
                var room = _context.Rooms.Where(x => x.Id == RoomId).FirstOrDefault();
                room.isDeleted = true;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating contact details", ex);
            }
        }
        public bool UpdateSocialMediaLinks(SocialMediaLinks model)
        {
            try
            {
                var links = _context.SocialMediaLinks.FirstOrDefault();

                if (links != null)
                {
                    links.FacebookLink = model.FacebookLink;
                    links.InstagramLink = model.InstagramLink;

                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    throw new Exception("No social media links found to update.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating social media links", ex);
            }
        }
      



    }
}
