using ApiAll.Model.pos;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital
{

    [Table("hospital_patient_search")]
    public class HospitalPatientSearch 
    {
        public long bemor_id { get; set; }
        public String bemor_fio { get; set; }

    }
}
