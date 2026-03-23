using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_user_note")]
    public class SkudUserNote : SkudBaseModel
    {
        public String note { get; set; }
        public DateTime reg_date { get; set; }
        public int? num_2 { get; set; } = 0;
        public String str_2 { get; set; }
        public long? userid { get; set; }
        public String user_name { get; set; }
    }
}
