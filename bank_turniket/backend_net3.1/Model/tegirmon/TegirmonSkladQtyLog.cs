using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_sklad_qty_log")]
    public class TegirmonSkladQtyLog : TegirmonBaseModel
    {
        // Ombor (sklad)
        public long TegirmonSkladId { get; set; }
        public TegirmonSklad Sklad { get; set; }

        // Mahsulot
        public long TegirmonProductId { get; set; }
        public TegirmonProduct Product { get; set; }

        // Miqdor (qancha o'zgargan)
        public double qty { get; set; }

        // Harakat turi: kirim, chiqim
        public long type { get; set; }  // 1- kirim, 2-chiqim

        public string text { get; set; }  // "Invoice_delete", "Invoice_update", "Check_update"

        // Narx (agar mavjud bo'lsa)
        public double price { get; set; } = 0.0;

        // Kim tomonidan amalga oshirilgan
        public long? userId { get; set; }  // Invoice yoki Check ID
        public long? authId { get; set; }
        public string user_name { get; set; }
        public string photo_url { get; set; }
        
        // Sana
        public DateTime create_date { get; set; } = DateTime.Now;

        // Izoh
        public string note { get; set; }

        // Qaysi operatsiya (Invoice ID, Check ID va h.k.)
        public long? operation_id { get; set; }
        public string operation_type { get; set; }  // "Invoice", "Check"
    }
}

