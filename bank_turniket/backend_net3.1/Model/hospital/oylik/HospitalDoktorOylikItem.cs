using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital.bron
{
    [Table("hospital_doktor_oylik_item")]
    public class HospitalDoktorOylikItem : HospitalBaseStandartModel
    {
        public Users users { get; set; }
        public long UsersId { get; set; }
        public DateTime reg_date_tm { get; set; } = DateTime.Now;
        public bool accepted_status { get; set; } = false;
        public Authorization authorization { get; set; }
        public long AuthorizationId { get; set; }

        public double service_percentage { get; set; } = 0.0;
        public double service_price { get; set; } = 0.0;
        public double starsionar_percentage { get; set; } = 0.0;
        public double starsionar_price { get; set; } = 0.0;
        public double salary { get; set; } = 0.0;

        public String reserved_1 { get; set; }
        public String reserved_2 { get; set; }
        public String reserved_3 { get; set; }
        public String reserved_4 { get; set; }
        public String reserved_5 { get; set; }
        public String reserved_6 { get; set; }
        public String reserved_7 { get; set; }

        public double reserved_1_nuber { get; set; } = 0.0;
        public double reserved_2_nuber { get; set; } = 0.0;
        public double reserved_3_nuber { get; set; } = 0.0;
        public double reserved_4_nuber { get; set; } = 0.0;
        public double reserved_5_nuber { get; set; } = 0.0;
        public double reserved_6_nuber { get; set; } = 0.0;
        public double reserved_7_nuber { get; set; } = 0.0;

        public long? reserved_1_idm { get; set; } = 0;
        public long? reserved_2_idm { get; set; } = 0;
        public long? reserved_3_idm { get; set; } = 0;
        public long? reserved_4_idm { get; set; } = 0;

        public DateTime tolonagan_vaqt { get; set; } = DateTime.Now;

        public int tolonagan_oyi { get; set; } = 0;

    }
}
