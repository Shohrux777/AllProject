using System;
using System.Collections.Generic;

namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_invoice")]
    public class TegirmonInvoice : TegirmonBaseModel
    {
        public String note { get; set; }
        public String user_name { get; set; }
        public double summ { get; set; } = 0.0;
        public double dolg_summ { get; set; } = 0.0;
        public double credit_sum { get; set; } = 0.0;
        public long? check_number { get; set; } = 0;
        public long? kassa_id { get; set; } = 0;
        public String hisob_name { get; set; }
        public long? hisob_id { get; set; } = 0;
        public double? poluchit_summa { get; set; } = 0.0;
        public String poluchit_summa_str { get; set; }

        public double? pol_dollor_summa { get; set; } = 0.0;
        public String pol_dollor_sum_str { get; set; }
        public double? pol_kurs_sum { get; set; } = 0.0;
        public String pol_kurs_sum_str { get; set; }
        public double? pol_all_sum { get; set; } = 0.0;
        public String pol_all_sum_str { get; set; }
        public double? pol_product_price { get; set; } = 0.0;

        public long? prixod_status { get; set; } = 0;

        public String auth_user_name { get; set; }
        // public double? poluchit_dollor { get; set; } = 0.0;
        public long? TegirmonContragentid { get; set; }
        public TegirmonContragent contragent { get; set; }
        public long? TegirmonSkladid { get; set; }
        public TegirmonSklad sklad { get; set; }
        public long? TegirmonClientid { get; set; }
        public TegirmonClient client { get; set; }
        public long? TegirmonAuthid { get; set; }
        public TegirmonAuth auth { get; set; }
        public long? TegirmonCompanyid { get; set; }
        public TegirmonCompany company { get; set; }
        public long? TegirmonProductid { get; set; }
        public TegirmonProduct product { get; set; }
        public double qty_real { get; set; } = 0.0;
        public String status_inv_type_name { get; set;}
        public List<TegirmonInvoiceItem> item_list { get; set;}
        public String image_str_url { get; set; }
        public bool get_money_for_bugdoy_zaxira { get; set; } = false;
        public List<String> client_names_list { get; set; }
        public long? kassa_check_id { get; set; } = 0;
        public long? sklad_diff_id { get; set; } = 0;
        public double? zaxira_extra_amount { get; set; } = 0.0;
    }
}
