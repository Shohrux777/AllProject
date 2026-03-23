using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_settings")]
    public class SkudSettings : SkudBaseModel
    {
        public String name_1 { get; set; }
        public String name_2 { get; set; }
        public String name_3 { get; set; }
        public String name_4 { get; set; }
        public String name_5 { get; set; }

        public bool? status_1 { get; set; } = false;
        public bool? status_2 { get; set; } = false;
        public bool? status_3 { get; set; } = false;
        public bool? status_4 { get; set; } = false;
        public bool? status_5 { get; set; } = false;

        public long? reserved_id_1 { get; set; }
        public long? reserved_id_2 { get; set; }
        public long? reserved_id_3 { get; set; }
        public long? reserved_id_4 { get; set; }
        public long? reserved_id_5 { get; set; }
   
 
    }
}
