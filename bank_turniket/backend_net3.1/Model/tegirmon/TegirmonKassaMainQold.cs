using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_kassa_main_qoldiq")]
    public class TegirmonKassaMainQold : TegirmonBaseModel
    {
        public double cash { get; set; } = 0.0;
        public double dollor { get; set; } = 0.0;
        public double all_summ { get; set; } = 0.0;
        public double auth_name { get; set; } = 0.0;
        public long auth_id { get; set; }
        public DateTime created_date { get; set; } = DateTime.Now;
        public DateTime udate_date { get; set; } = DateTime.Now;
        public long? bot_id { get; set; }
        public String string1 { get; set; }
        public double reserve_val { get; set; } = 0.0;
    }
}
