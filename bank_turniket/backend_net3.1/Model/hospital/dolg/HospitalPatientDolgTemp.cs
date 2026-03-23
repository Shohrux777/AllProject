using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital
{

    [Table("hospital_dolg_inv_temp")]
    public class HospitalPatientDolgTemp 
    {
        public long? patient_id { get; set; } = 0;
        public long? dolg { get; set; } = 0;
        public String  fio { get; set; } 
        public String phone { get; set; } 

    }
}
