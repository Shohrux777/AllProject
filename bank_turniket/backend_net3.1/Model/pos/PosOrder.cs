using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_order")]
    public class PosOrder : PosBaseModel
    {
        public long PosUserid  { get; set; }
        public PosUser posUser { get; set; }
        public String order_number { get; set; } = "0"; 
        public long PosUserPermisionid { get; set; }
        public PosUserPermision posUserPermision { get; set; }

        public long PosUserPermisionWorkTypeid  { get; set; }
        public PosUserPermisionWorkType workType { get; set; }
        public DateTime for_date { get; set; } = DateTime.Now;
        public DateTime for_buy_date { get; set; } = DateTime.Now;

        public long? PosSkladid  { get; set; }
        public PosSklad sklad { get; set; }
        public List<PosOrderItem> posOrderItemList { get; set; }
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

        public bool finish_status { get; set; } = false;

        [NotMapped]
        public String work_type => workType != null ? workType.name : "";

        [NotMapped]
        public String user_fio => posUser != null ? posUser.fio : "";

        [NotMapped]
        public String sklad_name => sklad != null ? sklad.name : "";
    }
}
