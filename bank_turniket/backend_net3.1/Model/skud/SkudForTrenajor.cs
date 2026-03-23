using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("for_trenajor")]
    public class SkudForTrenajor
    {
        [Key]
        public long userid { get; set; }
        public int active_days { get; set; } = 0;

        [Column(TypeName = "date")]
        public DateTime b_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime e_date { get; set; }

        public int? enter_counts { get; set; } = 0;

    }
}
