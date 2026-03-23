using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_user_access")]
    public class TegirmonUserAccess : TegirmonBaseModel
    {
          public bool product { get; set; } = false;
          public bool product_edit { get; set; } = false;
          public bool buy { get; set; } = false;
          public bool product_olish { get; set; } = false;
          public bool zaxira { get; set; } = false;
          public bool taroz { get; set; } = false;
          public bool taroz_list { get; set; } = false;
          public bool big_tarozi { get; set; } = false;
          public bool sell { get; set; } = false;
          public bool ostatka { get; set; } = false;
          public bool status_1 { get; set; } = false;
          public bool status_2 { get; set; } = false;
          public bool status_3 { get; set; } = false;
          public bool status_4 { get; set; } = false;
          public bool status_5 { get; set; } = false;
          public bool optom_statis { get; set; } = false;
          public bool optom_pay { get; set; } = false;
          public bool optom_load { get; set; } = false;
          public long num_1 { get; set; } = 0;
          public long num_2 { get; set; } = 0;
          public long num_3 { get; set; } = 0;
          public long num_4 { get; set; } = 0;
          public String text_1 { get; set; }
          public String text_2 { get; set; }
          public long TegirmonUserid { get; set; }
          public TegirmonUser user { get; set; }
    }
}
