using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.pos
{
    [Table("pos_client_schot_item")]
    public class PosClientSchotItem : PosBaseModel
    {
        public double qty { get; set; } = 0.0;
        public double real_qty { get; set; } = 0.0;
        public DateTime reg_date { get; set; } = DateTime.Now;
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public PosClientSchot posClientSchot { get; set; }
        public long PosClientSchotid { get; set; }


    }
}

