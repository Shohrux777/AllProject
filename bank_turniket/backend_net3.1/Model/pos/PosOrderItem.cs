using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_order_item")]
    public class PosOrderItem: PosBaseModel
    {
        public long PosOrderid  { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public PosOrder posOrder { get; set; }
        public long PosProductid  { get; set; }
        public PosProduct posProduct { get; set; }
        public long  PosProductUnitMeasurmentid  { get; set; }
        public PosProductUnitMeasurment unitMeasurment { get; set; }
        public double qty { get; set; } = 0.0;
        public double qty1 { get; set; } = 0.0;
        public double qty2 { get; set; } = 0.0;
        public double qty3 { get; set; } = 0.0;
        public double qty4 { get; set; } = 0.0;
        public double qty5 { get; set; } = 0.0;
        public String note { get; set; }
        public bool extra { get; set; } = false;
        public long? PosAuthorizationid { get; set; }
        public PosAuthorization posAuthorization { get; set; }
        public DateTime for_date { get; set; } = DateTime.Now;
        public DateTime for_buy_date { get; set; } = DateTime.Now;
        public String reserve_itm1 { get; set; }
        public String reserve_itm2 { get; set; }
        public String reserve_itm3 { get; set; }
        public String reserve_itm4 { get; set; }

        public long? reserve_id1 { get; set; } = 0;
        public long? reserve_id2 { get; set; } = 0;
        public long? reserve_id3 { get; set; } = 0;
        public long? reserve_id4 { get; set; } = 0;
        public long? reserve_id5 { get; set; } = 0;
        public long? reserve_id6 { get; set; } = 0;

        [NotMapped]
        public String product_name => posProduct != null ? posProduct.name: "";

        [NotMapped]
        public String product_code => posProduct != null ? posProduct.code : "";

        [NotMapped]
        public String unitmeasurment_name => unitMeasurment != null ? unitMeasurment.name : "";

    }
}
