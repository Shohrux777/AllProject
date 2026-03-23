using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_client_payment_product_all_temp")]
    public class PosClientPaymentProductAllTemp 
    {
        public String client_name { get; set; }
        public String product_name { get; set; }
        public double? product_qty { get; set; } = 0.0;
        public double? payment_summ { get; set; } = 0.0;
        public double? payment_real_sum { get; set; } = 0.0;
        public double? payment_discount_sum { get; set; } = 0.0;
        public long? product_id { get; set; } = 0;

    }
}


