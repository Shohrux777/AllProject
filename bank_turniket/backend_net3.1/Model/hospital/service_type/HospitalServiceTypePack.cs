using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital

{
    [Table("service_type_pack")]
    public class HospitalServiceTypePack : BaseModel
    {
        public String name { get; set; }
        public String note { get; set; }
        public String note_1 { get; set; }
        public String note_2 { get; set; }
        public String note_3 { get; set; }

        public List<HospitalServiceTypePackItem> items { get; set; }
    }
}
