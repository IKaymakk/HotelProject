using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class RoomImages
    {
        public int ImageId { get; set; }
        public int RoomId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public bool isDeleted { get; set; }
    }
}
