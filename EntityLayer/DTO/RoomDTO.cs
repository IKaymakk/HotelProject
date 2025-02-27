using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public int? Capacity { get; set; }
        public bool? isMainPage { get; set; }
        public List<RoomImageDTO> RoomImages { get; set; } 
        public List<RoomFeatureDTO> RoomFeatures { get; set; } 
    }

}
