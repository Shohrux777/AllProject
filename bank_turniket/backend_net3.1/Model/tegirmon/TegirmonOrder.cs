using System;
using System.Collections.Generic;

namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_order")]
    public class TegirmonOrder : TegirmonBaseModel
    {
        public String shafyor_name { get; set; }
        public String car_nomer { get; set; }
        public String user_name { get; set; }
        public String kassir_name { get; set; }
        public String load_name { get; set; }
        public String auth_user_name { get; set; }
        public String client_name { get; set; }
        public String paid_status { get; set; }
        public long? TegirmonOrderClientid { get; set; }
        public TegirmonOrderClient client { get; set; }
        public String note { get; set; }
        public double sum { get; set; } = 0.0;
        public double dollor { get; set; } = 0.0;
        public double kurs { get; set; } = 0.0;
        public double? pay_progress { get; set; } = 0.0;
        public double? load_progress { get; set; } = 0.0;
        public DateTime create_date { get; set; } = DateTime.Now;
        public DateTime createdAt { get; set; } = DateTime.Now; // zakaz yaratildis
        public DateTime pickUpDate { get; set; } = DateTime.Now;
        public bool IsLoaded { get; set; } = false; // zakaz yuklandi mashinaga
        public DateTime LoadedAt { get; set; } = DateTime.Now;
        public bool IsPaid { get; set; } = false; // zakazga pul to'landi
        public bool IsPrepaid { get; set; } = false; // false bulsa hali pul tushmagan agar pul tulansa va shu joyida berilsa paid va prepaid ni true qilamiz
        public DateTime PaidAt { get; set; } = DateTime.Now;
        public bool IsClosed { get; set; } = false; // zakaz yopildi
        public DateTime ClosedAt { get; set; } = DateTime.Now;
        public bool? is_loading { get; set; } = false; // zakaz ortilyabdi
        public bool? is_paying { get; set; } = false; // zakazga pul qisman tulandi
        public bool? is_begin { get; set; } = false; // zakaz kirsin
        public bool? is_save_client { get; set; } = false; // mahsulot zahiraga olib quyish
        public bool? is_get_client { get; set; } = false; // zahiradan mahsulot bersa
        public long? TegirmonAuthid { get; set; }
        public TegirmonAuth auth { get; set; }
        public long? TegirmonSkladid { get; set; }
        public TegirmonSklad sklad { get; set; }
        public List<TegirmonOrderItem> item_list { get; set; }
        public String image_url { get; set; }
        public double? remaining_sum { get; set; } = 0.0;
        public double? remaining_usd { get; set; } = 0.0;

    }
}
