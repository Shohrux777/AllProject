using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("sababli")]
    public class SkudSababli
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public String sababi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? _begin { get; set; }

        [Column(TypeName = "date")]
        public DateTime _end { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan b_time { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan e_time { get; set; }
    }
}
