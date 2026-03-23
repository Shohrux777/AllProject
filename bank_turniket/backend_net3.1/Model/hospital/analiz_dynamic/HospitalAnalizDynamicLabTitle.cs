using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital.analiz
{
    [Table("hospital_analiz_dynamic_lab_title")]
    public class HospitalAnalizDynamicLabTitle : BaseModel
    {
        public HospitalAnalizDynamicmain hospitalAnalizDynamicmain { get; set; }
        public long HospitalAnalizDynamicmainId { get; set; }
        public String name { get; set; }
        public String name_title { get; set; }
        public String lab_name { get; set; }
        public bool lab_name_status { get; set; } = false;
        public String norma { get; set; }
        public bool norma_status { get; set; } = false;
        public String first_name { get; set; }
        public bool firs_name_status { get; set; } = false;
        public String unit_measurment { get; set; }
        public bool unit_measurment_status { get; set; } = false;
        public String extra { get; set; }
        public bool extra_status { get; set; } = false;
        public String extra1 { get; set; }
        public bool extra1_status { get; set; } = false;
        public String name_1 { get; set; }
        public String name_2 { get; set; }
        public String name_3 { get; set; }
        public String name_4 { get; set; }
        public String name_5 { get; set; }
        public String name_6 { get; set; }
        public String name_7 { get; set; }
        public String name_8 { get; set; }
        public String name_9 { get; set; }
        public String name_10 { get; set; }
        public String name_11 { get; set; }
        public String name_12 { get; set; }
        public String name_13 { get; set; }
        public String name_14 { get; set; }
        public String name_15 { get; set; }
        public String name_16 { get; set; }
        public String name_17 { get; set; }
        public String name_18 { get; set; }
        public String name_19 { get; set; }
        public String name_20 { get; set; }
        public String name_21 { get; set; }
        public String name_22 { get; set; }
        public String name_23 { get; set; }
        public String name_24 { get; set; }
        public String name_25 { get; set; }
        public String name_26 { get; set; }
        public String name_27 { get; set; }
        public String name_28 { get; set; }
        public String name_29 { get; set; }
        public String name_30 { get; set; }
        public String name_31 { get; set; }
        public String name_32 { get; set; }
        public String name_33 { get; set; }
        public String name_34 { get; set; }
        public String name_35 { get; set; }
        public String name_36 { get; set; }
        public String name_37 { get; set; }
        public String name_38 { get; set; }
        public String name_39 { get; set; }
        public String name_40 { get; set; }
        public String name_41 { get; set; }
        public String name_42 { get; set; }
        public String name_43 { get; set; }
        public String name_44 { get; set; }
        public DateTime create_date { get; set; } = DateTime.Now;
        public DateTime update_date { get; set; } = DateTime.Now;
        public String name_45 { get; set; }
        public String name_46 { get; set; }
        public String name_47 { get; set; }
        public String name_48 { get; set; }
        public String name_49 { get; set; }
        public String name_50 { get; set;}

    }
}
