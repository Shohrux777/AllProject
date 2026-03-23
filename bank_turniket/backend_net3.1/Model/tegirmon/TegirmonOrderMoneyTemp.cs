using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.tegirmon
{
    public class TegirmonOrderMoneyTemp
    {
        public double? cash { get; set; }
        public double? dollor { get; set; }
        public double? summ { get; set; }
        public double? profit_summ { get; set; }
        public double? real_sum { get; set; }
        public double? humo { get; set; }
        public double? uz_card { get; set; }
        public double? click { get; set; }
        public double? skidka { get; set; }
        public double? payme { get; set; }
        public double? paynet { get; set; }
        public double? uzumpay { get; set; }
        public double? sum_balance { get; set; }

        public double? dollor_balance { get; set; }


    }
}

/*
 SELECT 
sum(card) as card, 
sum(cash) as cash, 
sum(summ) as summ, 
sum(profit_summ) as profit_sum, 
sum(real_sum) as real_sum, 
sum(humo) as humo, 
sum(uz_card) as uz_card, 
sum(perchisleniya) as perchisleniya, 
sum(dolg) as dolg, 
sum(dolg_payed) as dolg_payed, 
sum(creadit_payed) as creadit_payed, 
sum(rasxod) as rasxod, 
sum(online) as online, 
sum(salary) as salary, 
sum(for_buy_tovar_rasxod) as for_buy_tovar_rasxod, 
sum(srogi_otganlar_uchun_rasxod) as srogi_otganlar_uchun_rasxod
FROM public.tegirmon_check WHERE create_date BETWEEN '2022-01-01 00:00:00' AND '2022-08-08 00:00:00'
 
 */