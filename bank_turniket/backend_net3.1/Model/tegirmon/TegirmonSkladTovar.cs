using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_sklad_tovar")]
    public class TegirmonSkladTovar : TegirmonBaseModel
    {
          // Ombor (Sklad) FK
        public long TegirmonSkladId { get; set; }
        public TegirmonSklad Sklad { get; set; }

        // Mahsulot FK
        public long TegirmonProductId { get; set; }
        public TegirmonProduct Product { get; set; }

        // Miqdor (omborda qancha bor)
        public double qty { get; set; } = 0.0;
        public double? real_qty { get; set; } = 0.0;

        // Ixtiyoriy qo‘shimcha maydonlar
        public double LastPrice { get; set; } = 0.0; // oxirgi kirim narxi
        public DateTime UpdatedDate { get; set; } = DateTime.Now; // oxirgi yangilanish
        public long num_1 { get; set; } = 0;
        public long num_2 { get; set; } = 0;
          

    }
}
