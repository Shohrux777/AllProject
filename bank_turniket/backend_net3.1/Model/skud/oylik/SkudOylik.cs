using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("oylik")]
    public class SkudOylik:SkudBaseModel
    {
        public String name { get; set; }
        public double value { get; set; } = 0.0;
        public double reserved_value { get; set; } = 0.0;
        public double reserved_value2 { get; set; } = 0.0;
        public double shtaraf { get; set; } = 0.0; 
 
    }
}
