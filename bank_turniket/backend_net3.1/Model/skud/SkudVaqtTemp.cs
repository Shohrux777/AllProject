using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_vaqt_temp")]
    public class SkudVaqtTemp 
    {
        public String vaqt { get; set; }
    }
}
