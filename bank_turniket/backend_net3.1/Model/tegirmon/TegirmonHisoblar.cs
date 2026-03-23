using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_hisoblar")]
    public class TegirmonHisoblar : TegirmonBaseModel
    {
          public String name { get; set; }
          public bool status_1 { get; set; } = false;
          public bool status_2 { get; set; } = false;
          public bool status_3 { get; set; } = false;
          public double num_1 { get; set; } = 0;
          public double num_2 { get; set; } = 0;
          public double num_3 { get; set; } = 0;
          public String text_1 { get; set; }
          public String text_2 { get; set; }
    }
}
