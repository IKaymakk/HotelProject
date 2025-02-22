using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTO
{
    public class RoomCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Room> Rooms { get; set; } = new();

    }

}
