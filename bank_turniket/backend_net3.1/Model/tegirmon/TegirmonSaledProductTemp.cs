using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.tegirmon
{
    public class TegirmonSaledProductTemp
    {
        public String product_name { get; set; }
        public long? product_id { get; set; } = 0;
        public double? total_real_qty { get; set; } = 0.0;
        public double? total_saled_summa { get; set; } = 0.0;
        public double? total_profit_summ { get; set; } = 0.0;
    }
}


// using System;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace ApiAll.Model.tegirmon
// {
//     [NotMapped]
//     [Table("tegirmon_saled_product_summ_qty")]
//     public class TegirmonSaledProductTemp
//     {
//         public String product_name { get; set; }
//         public double? total_qty { get; set; } = 0.0;
//         public double? total_saled_summa { get; set; } = 0.0;
//         public double? total_profit_summ { get; set; } = 0.0;

//         [NotMapped]
//         public String total_qty_str => String.Format("{0:N}", total_qty);
//         [NotMapped]
//         public String total_saled_sum_str => String.Format("{0:N}", total_saled_sum);

//         [NotMapped]
//         public String total_prixod_price_str => String.Format("{0:N}", total_prixod_price);
//     }
// }

