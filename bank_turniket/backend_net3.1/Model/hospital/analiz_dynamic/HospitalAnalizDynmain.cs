using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital.analiz
{
    [Table("hospital_analiz_dyn_main")]
    public class HospitalAnalizDynmain : BaseModel
    {
        public String name { get; set; }
        public String name_title { get; set; }
        public String first_name { get; set; }
        public bool firs_name_status { get; set; } = false;
        public String lab_name { get; set; }
        public bool lab_name_status { get; set; } = false;
        public String norma { get; set; }
        public bool norma_status { get; set; } = false;
        public String unit_measurment { get; set; }
        public bool unit_measurment_status { get; set; } = false;
        public String extra { get; set; }
        public bool extra_status { get; set; } = false;
        public String extra1 { get; set; }
        public bool extra1_status { get; set; } = false;
        public bool status { get; set; } = false;
        public bool status1 { get; set; } = false;
        public bool status2 { get; set; } = false;
        public bool status3 { get; set; } = false;
        public bool status4 { get; set; } = false;
        public bool status5 { get; set; } = false;
        public String proba { get; set; }
        public String name_1 { get; set; }
        public String name_2 { get; set; }
        public String name_3 { get; set; }
        public String name_4 { get; set; }
        public String name_5 { get; set; }
        public String name_6 { get; set; }
        public String name_7 { get; set; }
        public DateTime create_date { get; set; } = DateTime.Now;
        public DateTime update_date { get; set; } = DateTime.Now;


    }
}
