using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("door_checkinout")]
    public class SkudDoorCheckinout
    {
        public long userid { get; set; }

        [Column(TypeName = "date")]
        public DateTime sana { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan checktime { get; set; }
        [StringLength(2)]
        public String checktype { get; set; }
        public String door_name { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long code { get; set; }
        public String begona { get; set; }
    }
}
