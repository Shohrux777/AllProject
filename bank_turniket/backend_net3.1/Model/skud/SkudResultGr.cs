using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("result_gr")]
    public class SkudResultGr:SkudBaseModel
    {
        [Column(TypeName = "time")]
        public TimeSpan? ish_b { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? ish_t { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? obed_b { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? obed_t { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? kech_keldi { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? vox_ketdi { get; set; }
        public String g_nomi { get; set; }
        public int? kun_nomi { get; set; }


    }
}
