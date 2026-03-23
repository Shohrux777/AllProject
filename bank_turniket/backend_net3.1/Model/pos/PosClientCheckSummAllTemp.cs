using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_client_check_summ_all_temp")]
    public class PosClientCheckSummAllTemp 
    {
        public String client_name { get; set; }
        public double? check_summ { get; set; } = 0.0;
        public double? check_card_sum { get; set; } = 0.0;
        public double? check_cash_sum { get; set; } = 0.0;
        public double? check_payme_sum { get; set; } = 0.0;
        public double? check_click_sum { get; set; } = 0.0;
        public double? check_online_sum { get; set; } = 0.0;
        public double? check_humo_sum { get; set; } = 0.0;
        public double? check_mobil_sum { get; set; } = 0.0;
        public double? check_discount_summ { get; set; } = 0.0;
        public double? check_bonus_summ { get; set; } = 0.0;
        public double? check_profit_summ { get; set; } = 0.0;


    }
}




