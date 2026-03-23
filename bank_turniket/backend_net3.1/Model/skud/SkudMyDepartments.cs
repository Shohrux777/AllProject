using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
    [Table("my_departments")]
    public class SkudMyDepartments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long deptid { get; set; }
        public String deptname { get; set; }
        public long? supdeptid { get; set; }
        public long? code { get; set; }
        public SkudCompany skudCompany { get; set; }
        public long? SkudCompanyid { get; set; }


    }
}
