using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model
{
    [Table("bot_language")]
    public class BotLanguage : BaseModel
    {
        public  String language_type_name { get; set; }
        public long bot_id { get; set; } = 0;
        public long language_id { get; set; } = 1;


    }
}
