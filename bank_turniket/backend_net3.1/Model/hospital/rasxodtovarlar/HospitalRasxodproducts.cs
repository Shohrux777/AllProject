using ApiAll.Model.market;
using ApiAll.Model.pos;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital
{

    [Table("hospital_rasxod_products")]
    public class HospitalRasxodproducts : HospitalBaseModel
    {
        public double qty { get; set; } = 0.0;
        public double real_qty { get; set; } = 0.0;

        public long MarketProductId { get; set; }
        public MarketProduct marketProduct { get; set; }

        public long ServiceTypeId { get; set; }
        public ServiceType serviceType { get; set; }

        public String note { get; set; }

        [NotMapped]
        public String product_name => marketProduct != null ? marketProduct.Name : "";





    }
}
