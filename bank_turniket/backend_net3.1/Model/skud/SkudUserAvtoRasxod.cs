using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_user_avto_rasxod")]
    public class SkudUserAvtoRasxod : SkudBaseModel
    {
        public int day { get; set; } = 0;
        public long? userid { get; set; }
        public double sum { get; set; } = 0.0;
        public String user_name { get; set; }
        public String note { get; set; }

        // har ehtimolga qarshi qushimcha column
        public String str_1 { get; set; }
        public DateTime reg_date { get; set; }



    }
}
