using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_user_day_add")]
    public class SkudUserDayAdd : SkudBaseModel
    {
        public String note { get; set; }
        public double count { get; set; } = 0.0;
        public DateTime reg_date { get; set; }
        public int? num_1 { get; set; } = 0;
        public int? num_2 { get; set; } = 0;
        public String str_1 { get; set; }
        public String str_2 { get; set; }
        public long? userid { get; set; }
        public String user_name { get; set; }
    }
}
