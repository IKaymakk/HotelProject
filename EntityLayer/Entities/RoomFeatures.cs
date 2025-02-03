
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class RoomFeatures
    {
        public int FeatureId { get; set; }
        public int RoomId { get; set; }
        public string Features { get; set; }
        public bool isDeleted { get; set; }
    }
}
