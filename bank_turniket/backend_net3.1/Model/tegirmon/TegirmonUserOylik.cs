using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_user_oylik")]
    public class TegirmonUserOylik : TegirmonBaseModel
    {
          public long userid { get; set; }
          public double sum { get; set; } = 0.0;
          public double num { get; set; } = 0.0;
          public String note { get; set; }
          public bool status { get; set; } = true;
        
    }
}
