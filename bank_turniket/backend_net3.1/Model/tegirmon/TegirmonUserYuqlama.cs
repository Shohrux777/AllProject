using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_user_yuqlama")]
    public class TegirmonUserYuqlama : TegirmonBaseModel
    {
          public long? userid { get; set; }
          public bool status { get; set; } = true;
          public DateTime K_date { get; set; }
          public DateTime created_date { get; set; } = DateTime.Now;
          public String note { get; set; }
    }
}
