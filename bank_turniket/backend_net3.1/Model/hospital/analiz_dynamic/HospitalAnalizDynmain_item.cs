using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital.analiz
{
    [Table("hospital_analiz_dyn_main_item")]
    public class HospitalAnalizDynmain_item : BaseModel
    {
        public HospitalAnalizDynmain HospitalAnalizDynmain { get; set; }
        public long HospitalAnalizDynmainId { get; set; }
        public String name { get; set; }
        public String first_name { get; set; }
        public String lab_name { get; set; }
        public String norma { get; set; }
        public String unit_measurment { get; set; }
        public String ed_izm { get; set; }
        public bool status1 { get; set; } = false;
        public bool status2 { get; set; } = false;
        public bool status3 { get; set; } = false;
        public bool status4 { get; set; } = false;
        public String name_1 { get; set; }
        public String name_2 { get; set; }
        public String name_3 { get; set; }
        public String name_4 { get; set; }
        public DateTime create_date { get; set; } = DateTime.Now;
        public DateTime update_date { get; set; } = DateTime.Now;

    }
}
