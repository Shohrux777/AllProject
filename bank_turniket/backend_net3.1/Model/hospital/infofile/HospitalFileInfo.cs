using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital.analiz
{
    [Table("hospital_file_info")]
    public class HospitalFileInfo : HospitalBaseModel
    {
        public String file_name { get; set; }
        public String file_extension_name { get; set; }
        
        public Authorization authorization { get; set; }
        public long? AuthorizationId { get; set; }

        public bool analiz_pic { get; set; } = false;


    }
}
