using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("smena")]
    public class SkudSmena:SkudBaseModel
    {
        
        public String smena_nomi { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan boshlanishi { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan tugashi { get; set; }   
        
        [Column(TypeName = "time")]
        public TimeSpan obed_b { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan obed_t { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan kech_keldi { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan vox_ketdi { get; set; }

 
    }
}
