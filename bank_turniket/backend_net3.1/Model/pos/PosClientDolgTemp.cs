using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_client_dolg_temp")]
    public class PosClientDolgTemp 
    {
        public String client_name { get; set; }
        public String clinet_phone_number { get; set; }
        public double? payed_summ { get; set; } = 0.0;
        public double? dolg_summ { get; set; } = 0.0;
        public long? client_id { get; set; }
        public String user_name { get; set; }

    }
}
