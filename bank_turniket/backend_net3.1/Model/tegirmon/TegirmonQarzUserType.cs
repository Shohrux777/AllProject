using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_qarz_user_type")]
    public class TegirmonQarzUserType : TegirmonBaseModel
    {
        public String name { get; set; }
        public double sum { get; set; } = 0.0;
        public double dollor { get; set; } = 0.0;
        public double all_summ { get; set; } = 0.0;
        public double kurs { get; set; } = 0.0;
        public String note { get; set; }
        public String image_url { get; set; }
        public String addiotionala_information { get; set; }
        public long? TegirmonQarzUserId { get; set; }
        public TegirmonQarzdorlikUser user { get; set; }

        public DateTime created_date { get; set; } = DateTime.Now;
        public String reserve { get; set; }

        public double reserve_val { get; set; } = 0.0;

    }
}
