using System;
using System.Collections.Generic;

namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_order_check")]
    public class TegirmonOrderCheck : TegirmonBaseModel
    {
        public DateTime create_date { get; set; } = DateTime.Now;
        public long? TegirmonOrderClientid { get; set; }
        public TegirmonOrderClient client { get; set; }
        public double card { get; set; } = 0.0;
        public double cash { get; set; } = 0.0;
        public double dollor { get; set; } = 0.0;
        public double summ { get; set; } = 0.0;
        public double profit_summ { get; set; } = 0.0;
        public double real_sum { get; set; } = 0.0;
        public double humo { get; set; } = 0.0;
        public double uz_card { get; set; } = 0.0;
        public double perchisleniya { get; set; } = 0.0;
        public double payme { get; set; } = 0.0;
        public double click { get; set; } = 0.0;
        public double paynet { get; set; } = 0.0;
        public double uzumpay { get; set; } = 0.0;
        public double online { get; set; } = 0.0;
        public double salary { get; set; } = 0.0;
        public double kurs { get; set; } = 0.0;
        public double skidka { get; set; } = 0.0;
        public double sum_balance { get; set; } = 0.0;
        public double dollor_balance { get; set; } = 0.0;
        public bool isInCashbox { get; set; } = true;
        public String image_url { get; set; }
        public long status_id { get; set; }
        public String status_name { get; set; }
        public long hisob_id { get; set; }
        public String hisob_name { get; set; }
        public String? client_name { get; set; }
        public long? kassa_id { get; set; } = 0;
        public long? TegirmonAuthid { get; set; }
        public TegirmonAuth auth { get; set; }
        public long? TegirmonOrderid { get; set; }
        public TegirmonOrder order { get; set; }
        public double remaining_sum { get; set; } = 0.0;
        public double remaining_usd { get; set; } = 0.0;
        
    }
}
