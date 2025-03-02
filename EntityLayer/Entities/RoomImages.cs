using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    [Table("tbl_RoomImages")]
    public class RoomImages
    {
        [Key]
        public int ImageId { get; set; }
        [ForeignKey("tbl_Rooms")]
        public int RoomId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public bool isDeleted { get; set; }
        public bool isCoverImage { get; set; }
        public virtual Room Room { get; set; }
    }
}
