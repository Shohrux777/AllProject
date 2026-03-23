using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("without_gr")]
    public class SkudWithoutGr:SkudBaseModel
    {
        public String gr_name { get; set; }
        public TimeSpan min_limit_kunlik { get; set; }
        public TimeSpan max_limit_kunlik { get; set; }
        public TimeSpan min_limit_sutka { get; set; }
        public TimeSpan ish_soati_kunlik { get; set; }
        public TimeSpan ish_soati_max { get; set; }
        

    }
}
