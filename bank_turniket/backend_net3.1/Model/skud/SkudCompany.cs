using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("skud_company")]
    public class SkudCompany : SkudBaseModel
    {
        public String name { get; set; }
    }
}
