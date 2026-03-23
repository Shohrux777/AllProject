using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("gr_for_emp")]
    public class SkudGrForEmp
    {
        [Key]
        public long id_emp { get; set; }
        public String gr_nomi { get; set; }

        [Column(TypeName = "date")]
        public DateTime _begin { get; set; }

        [Column(TypeName = "date")]
        public DateTime _end { get; set; }

    }
}
