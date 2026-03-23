using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_product_price")]
    public class TegirmonProductPrice : TegirmonBaseModel
    {
        public String name { get; set; }
        public String code { get; set; }
        public double price { get; set; } = 0.0;
        public double qty { get; set; } = 0.0;
        public long type { get; set; }
        public String measure_str { get; set; }
        public DateTime created_date { get; set; } = DateTime.Now;
        public long TegirmonProductid { get; set; }
        public TegirmonProduct product { get; set; }
    }
}
