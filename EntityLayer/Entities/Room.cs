using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{

    [Table("tbl_Rooms")]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? ImageBase64 { get; set; }
        public bool isDeleted { get; set; }
        public bool isMainPage { get; set; }

        public List<RoomImages> RoomImages { get; set; }
    }
}
