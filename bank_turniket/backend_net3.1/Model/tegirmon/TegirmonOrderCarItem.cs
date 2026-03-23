using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_order_car_item")]
    public class TegirmonOrderCarItem : TegirmonBaseModel
    {
        public long TegirmonOrderCarid { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public TegirmonOrderCar orderCar { get; set; }   // ✅ shu yetishmayaptimodel
        public double qty { get; set; } = 0.0;
        public long TegirmonProductid { get; set; }
        public TegirmonProduct product { get; set; }
        public double sum { get; set; } = 0.0;
        public double real_sum { get; set; } = 0.0;
        public String sum_str { get; set; }
    }
}
