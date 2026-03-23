using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_order_client_product")]
    public class TegirmonOrderClientProduct : TegirmonBaseModel
    {
        public long TegirmonOrderClientId { get; set; }
        public TegirmonOrderClient client { get; set; }
        public String client_name { get; set; }
        public long TegirmonProductid { get; set; }
        public TegirmonProduct product { get; set; }
        public double qty { get; set; } = 0.0;
        public double real_qty { get; set; } = 0.0;
        public DateTime created_date { get; set; } = DateTime.Now;
    }
}
