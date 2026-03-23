using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model
{
    public class VozvratAlreadyPaidPaymentList:BaseModel
    {
        public String patientName { get; set; }
        public String SerivceTypeName { get; set; }
        public String ServiceGroupName { get; set; }
        public DateTime dateTime { get; set; }
        public long? paymentCreatorId { get; set; }
        public long? paymentDeletorId { get; set; }
        public long summa { get; set; }
        public String contragentName { get; set; }
        public DateTime? PayedDate { get; set; } = DateTime.Now;
        public long? AuthorizationId { get; set; }
        public Authorization authorization { get; set;}
        public long? serviceTypeId { get; set; }
        public String labGroup { get; set; }
        public String vozvrat_str1 { get; set; }
        public String vozvrat_str2 { get; set; }
        public String vozvrat_str3 { get; set; }
        public long? vozvrat_numb1 { get; set; }
        public long? vozvrat_numb2 { get; set; }
    }
}
