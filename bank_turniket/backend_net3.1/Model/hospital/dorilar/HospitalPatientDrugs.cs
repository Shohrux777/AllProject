using ApiAll.Model.pos;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital
{

    [Table("hospital_patient_drugs")]
    public class HospitalPatientDrugs : HospitalBaseModel
    {
        public double qty { get; set; } = 0.0;
        public double real_qty { get; set; } = 0.0;
        public bool get_drugs_status { get; set; } = false;

        public long AuthorizationId { get; set; }
        public Authorization authorization { get; set; }

        public long PosProductid { get; set; }
        public PosProduct posProduct { get; set; }

        public String note { get; set; }

        [NotMapped]
        public String product_name => posProduct != null ? posProduct.name : "";





    }
}
