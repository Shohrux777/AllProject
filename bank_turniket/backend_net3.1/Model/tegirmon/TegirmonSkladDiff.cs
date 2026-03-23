using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_sklad_diff")]
    public class TegirmonSkladDiff : TegirmonBaseModel
    {
        public long TegirmonSkladId { get; set; }
        public TegirmonSklad Sklad { get; set; }
        public long TegirmonProductId { get; set; }
        public TegirmonProduct Product { get; set; }
        public double ostatka { get; set; }
        public double qty { get; set; }
        public double diff { get; set; }
        public double? product_price { get; set; }
        public string unit { get; set; }
        public string product_name { get; set; }
        public string user_name { get; set; }
        public long auth_id { get; set; }
        public DateTime create_date { get; set; } = DateTime.Now;


    }
}
