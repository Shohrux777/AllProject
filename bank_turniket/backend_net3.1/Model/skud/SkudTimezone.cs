using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{

    [Table("timezone")]
    public class SkudTimezone:SkudBaseModel
    {
                public String nomi { get; set; }
                public TimeSpan du_b { get; set; }
                public TimeSpan du_e { get; set; }
                public TimeSpan se_b { get; set; }
                public TimeSpan se_e  { get; set; }
                public TimeSpan chor_b { get; set; }
                public TimeSpan chor_e { get; set; }
                public TimeSpan pay_b { get; set; }
                public TimeSpan pay_e { get; set; }
                public TimeSpan ju_b { get; set; }
                public TimeSpan ju_e { get; set; }
                public TimeSpan shan_b { get; set; }
                public TimeSpan shan_e { get; set; }
                public TimeSpan yak_b { get; set; }
                public TimeSpan yak_e { get; set; }

}
}
