using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{

    [Table("tbl_AboutUs")]
    public class AboutUs
    {
        public int Id { get; set; }
        public string? AboutUsHeader { get; set; }
        public string? AboutUsText { get; set; }
        public string? AboutUsHeader2 { get; set; }
        public string? AboutUsText2 { get; set; }
    }
}
