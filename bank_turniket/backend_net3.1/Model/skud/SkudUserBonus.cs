using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.skud
{
    [Table("skud_user_bonus")]
    public class SkudUserBonus : SkudBaseModel
    {
        public string note { get; set; }
        public double count { get; set; } = 0.0;
        public DateTime reg_date { get; set; }
        public int? num_1 { get; set; } = 0;
        public int? num_2 { get; set; } = 0;
        public string str_1 { get; set; }
        public string str_2 { get; set; }
        public long? userid { get; set; }
        public string user_name { get; set; }
    }
}
