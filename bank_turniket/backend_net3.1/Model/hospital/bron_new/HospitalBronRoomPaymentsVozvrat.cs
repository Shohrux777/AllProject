using System;

namespace ApiAll.Model.hospital.bron_new
{
    public class HospitalBronRoomPaymentsVozvrat : HospitalBaseStandartModel
    {
        public HospitalBronRoomPayments hospitalBronRoomPayments { get; set; }
        public long HospitalBronRoomPaymentsid { get; set; }
        public double vozvrat_sum { get; set; } =0.0;
        public double card_summ { get; set; } = 0.0;
        public double cash_summ { get; set; } = 0.0;
        public double reserved_number_1 { get; set; } =0.0;
        public double reserved_number_2 { get; set; } =0.0;
        public double reserved_number_3 { get; set; } =0.0;
        public double reserved_number_4 { get; set; } =0.0;
        public long days { get; set; } =0;
        public long days1 { get; set; } =0;
        public long days2 { get; set; } =0;
        public long days3 { get; set; } =0;
        public String reserved_note_1 { get; set; }
        public String reserved_note_2 { get; set; }
        public String reserved_note_3 { get; set; }
        public String reserved_note_4 { get; set; }
        public DateTime reg_date_time { get; set; } = DateTime.Now;

    }
}
