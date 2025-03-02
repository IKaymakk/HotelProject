using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    [Table("tbl_SocialMediaLinks")]

    public class SocialMediaLinks
    {
        public int Id { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
    }
}
