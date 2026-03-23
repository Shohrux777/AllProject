using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_client_dolg_all_temp")]
    public class PosClientDolgAllTemp 
    {
        public double? payed_summ { get; set; } = 0.0;
        public double? dolg_summ { get; set; } = 0.0;

    }
}
