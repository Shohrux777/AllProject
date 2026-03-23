using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_order_item")]
    public class TegirmonOrderItem : TegirmonBaseModel
    {
        public long TegirmonOrderid { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public TegirmonOrder order { get; set; }
        public double qty { get; set; } = 0.0;
        public double real_qty { get; set; } = 0.0;
        public double price { get; set; } = 0.0;
        public long price_type { get; set; }
        public long price_qty { get; set; }
        public long TegirmonProductid { get; set; }
        public TegirmonProduct product { get; set; }
        public double sum { get; set; } = 0.0;
        public double real_sum { get; set; } = 0.0;
        public double? all_qty { get; set; } = 0.0;
        public double? new_qty { get; set; } = 0.0;
        public bool? zaxira { get; set; } = false;
        public String sum_str { get; set; }
    }
}
