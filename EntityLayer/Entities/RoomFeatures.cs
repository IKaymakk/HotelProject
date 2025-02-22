
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    [Table("tbl_RoomFeatures")]
    public class RoomFeatures
    {
        [Key]
        public int FeatureId { get; set; }
        [ForeignKey("tbl_Rooms")]
        public int RoomId { get; set; }
        public string Features { get; set; }
        public bool isDeleted { get; set; }
        public virtual Room Room { get; set; }
    }
}
