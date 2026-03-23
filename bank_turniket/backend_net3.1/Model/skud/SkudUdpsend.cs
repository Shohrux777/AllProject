using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ApiAll.Model.skud
{

    
    [Table("udpsend")]
    public class SkudUdpsend:SkudBaseModel
    {
        [Required]
        public String nomi { get; set; }
        public bool csend { get; set; } = false;
    }
}
