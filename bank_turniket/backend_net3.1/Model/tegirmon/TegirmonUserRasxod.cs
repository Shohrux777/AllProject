using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_user_rasxod")]
    public class TegirmonUserRasxod : TegirmonBaseModel
    {
        public String worker_name { get; set; }
        public String passport_number { get; set; }
        public String phone_number { get; set; }
        public double rasxod { get; set; } = 0.0;
        public double prixod { get; set; } = 0.0;
        public double all_summ { get; set; } = 0.0;
        public String all_summ_string { get; set; }
        public long status_rasxod { get; set; } = 0;
        public double dollor { get; set; } = 0.0;
        public String dollor_string { get; set; }
        public double kurs { get; set; } = 0.0;
        public String note { get; set; }
        public String image_url { get; set; }
        public String addiotionala_information { get; set; }
        public long? TegirmonWorkerid { get; set; }
        public TegirmonWorker worker { get; set; }
        public DateTime created_date { get; set; } = DateTime.Now;
        public DateTime udate_date { get; set; } = DateTime.Now;
        public long? bot_id { get; set; }
        public String reserve { get; set; }
        public double reserve_val { get; set; } = 0.0;
        public long reserve_val_l { get; set; } = 0;
    }
}
