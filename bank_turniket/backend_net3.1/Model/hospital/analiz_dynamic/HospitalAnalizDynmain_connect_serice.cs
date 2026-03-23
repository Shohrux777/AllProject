using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital.analiz
{
    [Table("hospital_analiz_dyn_main_connect_service")]
    public class HospitalAnalizDyn_connect_serice : BaseModel
    {
        public HospitalAnalizDynmain_item HospitalAnalizDynmain_item { get; set; }
        public long HospitalAnalizDynmain_itemId { get; set; }
        public ServiceType serviceType { get; set; }
        public long ServiceTypeId  { get; set; }
        public String name { get; set; }
        public bool status { get; set; } = false;
        public String name_1 { get; set; }
        public DateTime create_date { get; set; } = DateTime.Now;
        public DateTime update_date { get; set; } = DateTime.Now;


    }
}
