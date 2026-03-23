using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_invoice_new")]
    public class TegirmonInvoiceNew : TegirmonBaseModel
    {
        public String note { get; set; }
        public String user_name { get; set; }
        public double summ { get; set; } = 0.0;
        public String group_name { get; set; }
        public long group_id { get; set; } = 0;
        public long send_group_id { get; set; } = 0;
        public String shafyor_name { get; set; }
        public String car_number { get; set; }
        public String shafyor_tel { get; set; }
        public double product_price { get; set; } = 0.0;
        public double payed_summ { get; set; } = 0.0;
        public double all_measure { get; set; } = 0.0;
        public double car_measure { get; set; } = 0.0;
        public double product_measure { get; set; } = 0.0;
        public double discount { get; set; } = 0.0;
        public bool pull_status { get; set; } = false;
        public bool rashshod_status { get; set; } = false;
        public bool finish_status { get; set; } = false;
        public String image_first_pull { get; set; }
        public String image_second_pull { get; set; }
        public String image_last_pull { get; set; }
        public DateTime date_first_pull { get; set; } = DateTime.Now;
        public DateTime date_second_pull { get; set; } = DateTime.Now;
        public DateTime date_last_pull { get; set; } = DateTime.Now;
        public long auth_first_pull_id { get; set; } = 0;
        public long auth_second_pull_id { get; set; } = 0;
        public long auth_last_pull_id { get; set; } = 0;
        public String auth_first_pull_name { get; set; }
        public String auth_second_pull_name { get; set; }
        public String auth_last_pull_name { get; set; }
        public double dolg_summ { get; set; } = 0.0;
        public double credit_sum { get; set; } = 0.0;
        public double qty_real { get; set; } = 0.0;
        public String text_1 { get; set; }
        public String text_2 { get; set; }
        public bool status_1 { get; set; } = false;
        public bool status_2 { get; set; } = false;
        public String status_inv_type_name { get; set; }
        [Column("TegirmonClientNewid")]
        public long? TegirmonClientNewid { get; set; }
        public TegirmonClientNew client { get; set; }
        [Column("tegirmonticketid")]
        public long? TegirmonTicketid { get; set; }
        public TegirmonTicket ticket { get; set; }
        public long? TegirmonAuthid { get; set; }
        public TegirmonAuth auth { get; set; }
        public long? TegirmonProductid { get; set; }
        public TegirmonProduct product { get; set; }
        public List<TegirmonInvoiceItemNew> item_list { get; set; }
        // public List<String> client_names_list { get; set; }
        // public long? TegirmonCompanyid { get; set; }
        // public TegirmonCompany company { get; set; }
        // public long? TegirmonContragentid { get; set; }
        // public TegirmonContragent contragent { get; set; }
        //  public bool get_money_for_bugdoy_zaxira { get; set; } = false;
        // public String status_inv_type_name { get; set; }
    }
}
