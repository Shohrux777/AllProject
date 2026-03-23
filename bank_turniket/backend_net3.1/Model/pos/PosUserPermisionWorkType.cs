using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_user_permission_work_type")]
    public class PosUserPermisionWorkType : PosBaseModel
    {
        public String name { get; set; }
        public String note { get; set; }
  

    }
}
