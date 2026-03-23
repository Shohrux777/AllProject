using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_user_ishlagan_puli")]
    public class TegirmonUserIshlaganPuli : TegirmonBaseModel
    {
          public long? userid { get; set; }
          public long? salary_id { get; set; }
          public TimeSpan work_time { get; set; }
          public double sum { get; set; } = 0.0;
          public double num { get; set; } = 0.0;
          public DateTime K_date { get; set; }
          public DateTime created_date { get; set; } = DateTime.Now;
          public String note { get; set; } 
          public String image_url { get; set; }
          public String? auth_name { get; set; }
          public double? dollor { get; set; } = 0.0;
          public double? hisoblan_sum { get; set; } = 0.0;
          public double? prixod_sum { get; set; } = 0.0;
          public double? rasxod_sum { get; set; } = 0.0;
          public double? old_qarz { get; set; } = 0.0;
          public double? old_debt { get; set; } = 0.0;
          public double? num1 { get; set; } = 0.0;
    }
}
