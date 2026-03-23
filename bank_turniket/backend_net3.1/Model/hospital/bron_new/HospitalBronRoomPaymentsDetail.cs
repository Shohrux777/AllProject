using System;

namespace ApiAll.Model.hospital.bron_new
{
    public class HospitalBronRoomPaymentsDetail : HospitalBaseStandartModel
    {
        public HospitalBronRoomPayments hospitalBronRoomPayments { get; set; }
        public long HospitalBronRoomPaymentsid { get; set; }
        public double payed_summ { get; set; } =0.0;
        public double card_summ { get; set; } = 0.0;
        public double cash_summ { get; set; } = 0.0;
        public double need_payed_summ { get; set; } =0.0;
        public double discount_payed_summ { get; set; } =0.0;
        public double vozvrat_sum { get; set; } = 0.0;
        public long? AuthorizationId { get; set; }
        public Authorization authorization { get; set; }
        public DateTime reg_date_time { get; set; } = DateTime.Now;

    }
}
