using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_qarz_user_rasxod")]
    public class TegirmonQarzUserRasxod : TegirmonBaseModel
    {
        public String worker_name { get; set; }
        public String passport_number { get; set; }
        public String phone_number { get; set; }
        public double sum { get; set; } = 0.0;
        public String sum_str { get; set; }
        public double all_summ { get; set; } = 0.0;
        public String all_summ_string { get; set; }
        public long status_rasxod { get; set; } = 0;
        public double dollor { get; set; } = 0.0;
        public String dollor_string { get; set;}
        public double kurs { get; set; } = 0.0;
        public String note { get; set; }
        public String image_url { get; set; }
        public String addiotionala_information { get; set;}
        public long? TegirmonQarzUserId { get; set; }
        public TegirmonQarzdorlikUser user { get; set; }
        public DateTime created_date { get; set; } = DateTime.Now;
        public DateTime udate_date { get; set; } = DateTime.Now;
        public long? bot_id { get; set; }
        public String reserve { get; set; }
        public String reserve1 { get; set; }
        public double reserve_val { get; set; } = 0.0;
        public double reserve_val_1 { get; set; } = 0.0;
        public double reserve_val_2 { get; set; } = 0.0;
    }
}
