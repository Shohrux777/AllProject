using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_kemagan_user_temp")]
    public class SkudKemaganUserTemp 
    {
        public String ism { get; set; }
        public String dept { get; set; }
        public long? userid { get; set; }
    }
}
