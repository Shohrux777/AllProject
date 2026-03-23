using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital.analiz
{
    [Table("hospital_analiz_group_info")]
    public class AnalizGroupInfo  : BaseModel
    {
        public String device_name { get; set; }
        public long tab_search_id { get; set; } = 0;
        public String name_1 { get; set; }
        public String name_2 { get; set; }
        public String name_3 { get; set; }
        public String name_4 { get; set; }
        public String name_5 { get; set; }
    }
}
