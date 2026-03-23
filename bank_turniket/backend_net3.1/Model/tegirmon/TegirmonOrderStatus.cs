using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_order_status")]
    public class TegirmonOrderStatus : TegirmonBaseModel
    {
        public String name { get; set; }
        public double qty { get; set; } = 0.0;
        public long type { get; set; }
        public String measure_str { get; set; }
        public DateTime created_date { get; set; } = DateTime.Now;
    }
}
