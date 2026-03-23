using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("lk")]
    public class SkudLk:SkudBaseModel
    {
        public String lkey { get; set; }
    }
}
