using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("my_userinfo")]
    public class SkudMyUserinfo
    {
        [Key]
        public long badgenumber { get; set; }

        [ForeignKey("res_badgenumber")]
        public SkudOylik skudOylik { get; set; }
        public long? res_badgenumber { get; set; }
        public long? userid { get; set; }
        public String ism { get; set; }
        public String cardno { get; set; }
        public String familiya { get; set; }
        public long? departid { get; set; }
        public String acc_name { get; set; }
        public String image_url { get; set; }
        public int? gr { get; set; } = 0;
        public List<long>? fpid { get; set; }
        public int? status { get; set; }
        public int? without_gr_id { get; set; } = 0;

        public String group_name { get; set; } = "";
        public String middle_name { get; set; } = "";
        public TimeSpan? lesson_start { get; set; }
        public TimeSpan? lesson_end { get; set; }
        public long? group_id { get; set; } = 0;
        public long? organization_id { get; set; } = 0;
        public long? student_id { get; set; } = 0;

        public String? passport { get; set; }
        public String? phone_number { get; set; }
        public String? address { get; set; }
        public String? born_date { get; set; }


        [NotMapped]
        public SkudMyDepartments departments { get; set; }

        [NotMapped]
        public String vaqt { get; set; } = "";

        [NotMapped]
        public String kun { get; set; } = "";

        [NotMapped]
        public String oylik_nomi => skudOylik != null ? skudOylik.name : "";

        [NotMapped]
        public double worked_hours_itm { get; set; } = 0.0;

        [NotMapped]
        public string worked_hours_itm_str { get; set; } = "";

        [NotMapped]
        public List<String> ishlagan_vaqtlar_ls { get; set; }

        [NotMapped]
        public double ishlagan_puli { get; set; } = 0.0;
        [NotMapped]
        public TimeSpan vaqt_kegan { get; set; }

        [NotMapped]
        public List<SkudMyCheckinout> skudMyCheckinouts { get; set; }

        [NotMapped]
        public TimeSpan? emp_checktime { get; set; }

        [NotMapped]
        public int taqqoshlash_status { get; set; } = -1;
        [NotMapped]
        public String ishlagan_vaqti_yangi { get; set; }
        [NotMapped]
        public String ishlagan_vaqti_yangi_time_format { get; set; }

        [NotMapped]
        public int? vaqt_flag { get; set; }
        
        [NotMapped]
        public string kelganidan_beri { get; set; }

        [NotMapped]
        public int? user_note { get; set; }

    }
}
