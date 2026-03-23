using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_user_hisob_access")]
    public class TegirmonUserHisobAccess : TegirmonBaseModel
    {
         
          public String name { get; set; }
          public String text_2 { get; set; }
          public long TegirmonUserid { get; set; }
          public TegirmonUser user { get; set; }
          public long TegirmonHisoblarId { get; set; }
          public TegirmonHisoblar hisob { get; set; }
    }
}
