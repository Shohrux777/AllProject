using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_client")]
    public class PosClient : PosBaseModel
    {
        [Required]
        public String fio { get; set; }
        [Required]
        public String phone_number { get; set; }
        public String passport_number { get; set; }
        public String address { get; set; }
        public String image { get; set; }
        public String note { get; set; }
        public String reserved_1 { get; set; }
        public String reserved_2 { get; set; }
        public String reserved_3 { get; set; }
        public String reserved_4 { get; set; }
        public String reserved_5 { get; set; }
        public String reserved_6 { get; set; }
        public bool? client_publk { get; set; } = true;

        [Required]
        public String card_number { get; set; }
        public long? bot_id { get; set; }
        public double? cashback_percentage { get; set; } = 0.0;
        public double discount_percantage { get; set; } = 0.0;
        public double? cash_sum { get; set; } = 0.0;
    }
}
