﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class RoomFeatures
    {
        [Key]
        public int FeatureId { get; set; }
        public int RoomId { get; set; }
        public string Features { get; set; }
        public bool isDeleted { get; set; }
    }
}
