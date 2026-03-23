using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_close")]
    public class TegirmonClose : TegirmonBaseModel
    {
        public bool status { get; set; } = false;
    }
}
