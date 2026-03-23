using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_user_permission_with_order")]
    public class PosUserPermisionWithOrder : PosBaseModel
    {
        public String name { get; set; }
        public String note { get; set; }
        public long? PosAuthorizationid { get; set; }
        public PosAuthorization posAuthorization { get; set; }
        public long PosUserid { get; set; }
        public PosUser posUser { get; set; }
        public bool signed_status { get; set; } = false;
        public bool finish_status { get; set; } = false;
        public DateTime first_date_time { get; set; } = DateTime.Now;
        public DateTime last_date_time { get; set; } = DateTime.Now;
        public long PosOrderid { get; set; }
        public PosOrder posOrder { get; set; }
        public int sort { get; set; } = 0;

        
        
 

    }
}
