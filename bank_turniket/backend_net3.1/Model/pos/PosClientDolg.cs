using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_client_dolg")]
    public class PosClientDolg : PosBaseModel
    {
        public String note { get; set; }
        public String note1 { get; set; }
        public String reserved_1 { get; set; }
        public String reserved_2 { get; set; }
        public String reserved_3 { get; set; }
        public String reserved_4 { get; set; }
        public String reserved_5 { get; set; }
        public String reserved_6 { get; set; }
        public double payed_summ { get; set; } = 0.0;
        public double dolg_summ { get; set; } = 0.0;
        public long? PosCheckid { get; set; }
        public PosCheck check { get; set; }
        public long PosClientid { get; set; }
        public PosClient client { get; set; }
        public DateTime created_date_tm { get; set; } = DateTime.Now;
        public DateTime limit_date_time { get; set; } = DateTime.Now;
        public long limit_days { get; set; } = 0;
        public bool? show_publik { get; set; } = true;
        public long? bot_id { get; set; }
        public double? cashback_percentage { get; set; } = 0.0;
        public double discount_percantage { get; set; } = 0.0;
        public double? cash_sum { get; set; } = 0.0;
    }
}
