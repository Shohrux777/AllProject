using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_user_sklad_access")]
    public class TegirmonUserSkladAccess : TegirmonBaseModel
    {
         
          public String name { get; set; }
          public String text_2 { get; set; }
          public long TegirmonUserid { get; set; }
          public TegirmonUser user { get; set; }
          public long TegirmonSkladId { get; set; }
          public TegirmonSklad sklad { get; set; }
    }
}
