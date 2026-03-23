using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_category")]
    public class TegirmonCategory : TegirmonBaseModel
    {
        public String name { get; set; }
        public String phone_number { get; set; }
        public String color { get; set; }
        public long? bot_id { get; set; }
    }
}
