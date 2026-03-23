using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital.bron_new
{
    public class HospitalBronRoomPaymentsDetailTemp 
    {
        [NotMapped]
        public HospitalBronRoomPayments hospitalBronRoomPayments { get; set; }
        public long? id_payment { get; set; }
        public double? card_sum { get; set; } = 0.0;
        public double? cash_sum { get; set; } = 0.0;
        public double? vozvrat_sum { get; set; } = 0.0;

        [NotMapped]
        public double payed_summ => hospitalBronRoomPayments!= null ? hospitalBronRoomPayments.payed_summ: 0.0;
        [NotMapped]
        public double reserved_number_db_5 => hospitalBronRoomPayments!= null ? hospitalBronRoomPayments.reserved_number_db_5 : 0.0;
        [NotMapped]
        public double price_for_one_day => hospitalBronRoomPayments != null ? hospitalBronRoomPayments.price_for_one_day : 0.0;
        [NotMapped]
        public long reserved_number_2 => hospitalBronRoomPayments != null ? hospitalBronRoomPayments.reserved_number_2 : 0;

        [NotMapped]
        public double ovqat_puli { get; set; } = 0.0;
        [NotMapped]
        public double qolgan_pul { get; set; } = 0.0;
      
    }
}
