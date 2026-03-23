using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_kech_qolgan_temp")]
    public class SkudKechQolganTemp
    {
        public TimeSpan? emp_checktime { get; set; }
    }
}
