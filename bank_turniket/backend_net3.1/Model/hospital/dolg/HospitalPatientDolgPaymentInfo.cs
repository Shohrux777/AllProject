using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital
{

    [Table("hospital_dolg_inv_payment_info")]
    public class HospitalPatientDolgPaymentInfo : BaseModel
    {
        public long? creatorAuthId { get; set; }
        public long PatientsId { get; set; }
        public Patients patients { get; set; }
        public DateTime dolgDate { get; set; } = DateTime.Now;
        public Payments payments { get; set; }
        public long PaymentsId { get; set; }
        [NotMapped]
        public String patient_name => patients != null ? patients.FIO : "";
        [NotMapped]
        public long? dolg_summ => payments != null ? payments.dolg_summ : 0;

        [NotMapped]
        public String service_name => payments != null ? (payments.serviceType != null ? payments.serviceType.Name : "") : "";
        

    }
}
