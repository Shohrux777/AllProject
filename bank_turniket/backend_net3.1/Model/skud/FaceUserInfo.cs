using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("face_userinfo")]
    public class FaceUserInfo
    {
        [Key]
        public long id { get; set; }
        public String image { get; set; } = "";
        public String group_name { get; set; } = "";
        public String first_name { get; set; } = "";
        public String last_name { get; set; } = "";
        public String middle_name { get; set; } = "";
        public TimeSpan? lesson_start { get; set; } 
        public TimeSpan? lesson_end { get; set; }
        public long? group_id { get; set; } = 0;
        public long? organization_id { get; set; } = 0;
        public long? student_id { get; set; } = 0;

    }
}
