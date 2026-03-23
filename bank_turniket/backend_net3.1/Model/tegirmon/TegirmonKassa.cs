using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_kassalar")]
    public class TegirmonKassa : TegirmonBaseModel
    {
          public String name { get; set; }
          public bool status_1 { get; set; } = false;
          public bool status_2 { get; set; } = false;
          public bool status_3 { get; set; } = false;
          public long num_1 { get; set; } = 0;
          public long num_2 { get; set; } = 0;
          public long num_3 { get; set; } = 0;
          public String text_1 { get; set; }
          public String text_2 { get; set; }
          public long? TegirmonUserid { get; set;}
          public TegirmonUser user { get; set;}
    }
}
