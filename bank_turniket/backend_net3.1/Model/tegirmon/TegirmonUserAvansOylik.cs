using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_user_avans_oylik")]
    public class TegirmonUserAvansOylik : TegirmonBaseModel
    {
          public long? userid { get; set; }
          public long? salary_id { get; set; }
          public double sum { get; set; } = 0.0;
          public double dollor { get; set; } = 0.0;
          public double num { get; set; } = 0.0; // 1 oylik 2 avans
          public DateTime b_date { get; set; }
          public DateTime e_date { get; set; }
          public DateTime created_date { get; set; } = DateTime.Now;
          public String note { get; set; } 
          public String image_url { get; set; }
          public String? auth_name { get; set; }
          public long? auth_id { get; set; }
          public bool avans { get; set; } = false; // bu avans true bulsa avansi oylik hisoblan ishlagan pulidan olib qolinganini bildiradi
          public bool salary { get; set; } = false; // bu oylik yoki avans ekanligini bildiradi
          public double? hisoblan_sum { get; set; } = 0.0;
          public double? qarz_sum { get; set; } = 0.0;
          public double? additional_qarz_sum { get; set; } = 0.0;
          public double? hisoblan_dollor { get; set; } = 0.0;
          public double? qarz_dollor { get; set; } = 0.0;
          public double? additional_qarz_dollor { get; set; } = 0.0;
          public double? old_qarz { get; set; } = 0.0;
          public double? old_debt { get; set; } = 0.0;
          public double? num1 { get; set; } = 0.0;
    }
}
