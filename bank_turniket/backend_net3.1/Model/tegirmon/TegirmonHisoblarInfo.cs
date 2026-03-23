using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_hisoblar_info")]
    public class TegirmonHisoblarInfo : TegirmonBaseModel
    {
          public double cash { get; set; } = 0;
          public double dollor { get; set; } = 0;
          public double perichsleniya { get; set; } = 0;
          public double before_cash { get; set; } = 0;
          public double before_dollor { get; set; } = 0;
          public double before_perichsleniya { get; set; } = 0;
          public double num_1 { get; set; } = 0;
          public double num_2 { get; set; } = 0;
          public String text_1 { get; set; }
          public String text_2 { get; set; }
          public bool status_1 { get; set; } = false;
          public long? TegirmonHisoblarid { get; set;}
          public TegirmonHisoblar hisob { get; set;}
    }
}
