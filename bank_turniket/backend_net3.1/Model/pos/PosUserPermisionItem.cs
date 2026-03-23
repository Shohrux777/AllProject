using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_user_permission_item")]
    public class PosUserPermisionItem : PosBaseModel
    {
        public long PosUserPermisionid { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public PosUserPermision posUserPermision { get; set; }

        public long PosUserid  { get; set; }
        public PosUser posUser { get; set; }

        public String note { get; set; }
        public bool finish { get; set; } = false;
        public bool changable_status { get; set; } = false;

        public int sort { get; set; } = 0;

        [NotMapped]
        public String user_name => posUser != null ? posUser.fio : "";
      

    }
}
