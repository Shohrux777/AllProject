using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("my_checkinout")]
    public class SkudMyCheckinout
    {

        public SkudMyCheckinout() {
            status = false;
        }
        public long userid { get; set; }

        [Column(TypeName = "date")]
        public DateTime sana { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan checktime { get; set; }

        [StringLength(2)]
        public String checktype { get; set; }
        public String door_name { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long code { get; set; }
        public String begona { get; }

        [JsonProperty("allowUploading", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool? status { get; set; } = false;

        [NotMapped]
        public SkudMyUserinfo userinfo { get; set; }

        [NotMapped]
        public String fio => userinfo != null ? (userinfo.familiya + " " + userinfo.ism) : "";



    }
}
