using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_client_dolg_history")]
    public class PosClientDolgHistory : PosBaseModel
    {
        public long PosClientDolgid { get; set; }
        public PosClientDolg clientDolg { get; set; }
        public double payed_summ { get; set; } = 0.0;
        public long? PosCheckid { get; set; }
        public PosCheck check { get; set; }
        public DateTime created_date_tm { get; set; } = DateTime.Now;
  
    }
}
