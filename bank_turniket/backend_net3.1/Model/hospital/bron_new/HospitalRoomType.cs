namespace ApiAll.Model.hospital.bron_new
{
    public class HospitalRoomType : HospitalBaseStandartModel
    {
        public string name { get; set; }
        public double room_price{ get; set; } = 0.0;
        public double room_bed_price { get; set; } = 0.0;
        public double room_bed_price_not_patient { get; set; } = 0.0;
        public double? ovqat_puli_patient { get; set; } = 0.0;
        public double? ovqat_puli_room { get; set; } = 0.0;
        public double? ovqat_puli_qarovchi { get; set; } = 0.0;
        public double? doctor_price { get; set; } = 0.0;
        public double? service_group { get; set; } = 0.0;

        public long? service_group_number { get; set; } = 0;
        public long? reserved_number_2 { get; set; } = 0;
        public long? reserved_number_3 { get; set; } = 0;
        public long? reserved_number_4 { get; set; } = 0;
        public long? reserved_number_5 { get; set; } = 0;

        public string reserved_name_1 { get; set; }
        public string reserved_name_2 { get; set; }
        public string reserved_name_3 { get; set; }
        public string reserved_name_4 { get; set; }
        public string reserved_name_5 { get; set; }

        public double? reserved_number_db_1 { get; set; } = 0.0;
        public double? reserved_number_db_2 { get; set; } = 0.0;
        public double? reserved_number_db_3 { get; set; } = 0.0;
        public double? reserved_number_db_4 { get; set; } = 0.0;
        public double? reserved_number_db_5 { get; set; } = 0.0;

        public bool? reserved_status_1 { get; set; } = false;
        public bool? reserved_status_2 { get; set; } = false;
        public bool? reserved_status_3 { get; set; } = false;
        public bool? reserved_status_4 { get; set; } = false;
        public bool? reserved_status_5 { get; set; } = false;
    }
}
