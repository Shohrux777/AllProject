using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model
{
    public class Payments : BaseModel
    {
        public String Name { get; set; }
        public String ServiceName { get; set; }
        public String PatientName { get; set; }
        public long ServiceTypeId { get; set; }
        public ServiceType serviceType { get; set; }
        public long Summ { get; set; }
        public long ReserveSumm { get; set; }
        public long PaymentInCash { get; set; }
        public long PaymentInCard { get; set; }
        public long PatientsId { get; set; }
        public Patients patients { get; set; }
        [DefaultValue(false)]
        public bool Finish { get; set; } = false;
        public long AuthorizationId { get; set; }
        public Authorization authorization { get; set; }
        public long ContragentId { get; set; }
        public Contragent contragent { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime RegistratedDate { get; set; } = DateTime.Now;
        public bool FinishPayment { get; set; }
        public DateTime acceptanceDateTime { get; set; }
        public DateTime PayedDate { get; set; } = DateTime.Now;
        public long? creatorAuthId { get; set; }

        public double? discount_qty { get; set; } = 0.0;
        public String discount_card_qty { get; set; }
        public double? discount_real_qty { get; set; } = 0.0;
        public double? discount_persantage_qty { get; set; } = 0.0;

        public long? dolg_summ { get; set; } = 0;
        public bool? dolg_status { get; set; } = false;

        public long? payed_auth_id { get; set; }
        public long? pay_num1 { get; set; } = 0;
        public long? pay_num2 { get; set; } = 0;
        public bool? pay_status { get; set; } = false;
        public String pay_str { get; set; }

        public long? analiz_hos_id { get; set; } = 0;
        public long? analiz_hos_num { get; set; } = 0;
    }
}
