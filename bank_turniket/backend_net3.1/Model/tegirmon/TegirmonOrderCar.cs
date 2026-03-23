using System;
using System.Collections.Generic;

namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_order_car")]
    public class TegirmonOrderCar : TegirmonBaseModel
    {
        public String shafyor_name { get; set; }
        public String car_nomer { get; set; }
        public String phone_number { get; set; }
        public long? TegirmonOrderid { get; set; }
        public TegirmonOrder order { get; set; }
        public String note { get; set; }
        public DateTime create_date { get; set; } = DateTime.Now;
        public List<TegirmonOrderCarItem> item_list { get; set; }
        public String image_url { get; set; }
        public long? TegirmonAuthid { get; set; }
        public TegirmonAuth auth { get; set; }
        public bool? is_begin { get; set; } = false; // zakaz kirsin
        public String? user_name { get; set; }

    }
}
