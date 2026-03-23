using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_user_permission")]
    public class PosUserPermision : PosBaseModel
    {
        public String name { get; set; }
        public String note { get; set; }
        public bool auto_select { get; set; } = false;
        public List<PosUserPermisionItem> posUserPermisionItems { get; set; }

    }
}
