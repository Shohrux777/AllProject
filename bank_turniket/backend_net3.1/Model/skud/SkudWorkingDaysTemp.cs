using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_user_working_days_count_temp")]
    public class SkudWorkingDaysTemp
    {
        public String full_name { get; set; }
        public long? user_id { get; set; } = 0;
        public long? cur_day_count { get; set; } = 0;
        public long? working_days_count { get; set; } = 0;
       

    }
}
