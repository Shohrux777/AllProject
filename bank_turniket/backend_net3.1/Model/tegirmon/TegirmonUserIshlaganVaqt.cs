using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_user_ishlagan_vaqt")]
    public class TegirmonUserIshlaganVaqt : TegirmonBaseModel
    {
          public long? userid { get; set; }
          public TimeSpan work_time { get; set; }
          public DateTime K_date { get; set; }
          public DateTime created_date { get; set; } = DateTime.Now;
          public String note { get; set; } 

    }
}
