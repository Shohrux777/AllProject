using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("tablename")]
    public class SkudTablename: SkudBaseModel
    {
        
        public byte _data { get; set; }
    }
}
