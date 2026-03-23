using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_sklad_history")]
    public class TegirmonSkladHistory : TegirmonBaseModel
    {
        // Ombor (sklad)
        public long TegirmonSkladId { get; set; }
        public TegirmonSklad Sklad { get; set; }

        // Mahsulot
        public long TegirmonProductId { get; set; }
        public TegirmonProduct Product { get; set; }

        // Miqdor (qancha kirgan yoki chiqqan)
        public double qty { get; set; }

        // Harakat turi: kirim, chiqim, ko‘chirish
        public long type { get; set; }  // 1- kirim, 2-chiqim, 3-kirim ko'chirish 4-chiqim ko'chirish

        public string text { get; set; }  // "kirim", "chiqim", "ko'chirish"

        // Narx (kirim narxi yoki sotuv narxi)
        public double price { get; set; } = 0.0;

        // Kim tomonidan amalga oshirilgan
        public long? userId { get; set; }
        public long? authId { get; set; }
        public string user_name { get; set; }
        public string photo_url { get; set; }
        // Sana
        public DateTime create_date { get; set; } = DateTime.Now;

        // Izoh
        public string note { get; set; }
          

    }
}
