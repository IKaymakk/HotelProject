﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{



    [Table("tbl_RoomCategory")]
    public class RoomCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Room> Rooms { get; set; } = new();
    }
}


