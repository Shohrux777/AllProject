using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.hospital
{
    [Table("service_type_pack_item")]
    public class HospitalServiceTypePackItem : BaseModel
    {
        public HospitalServiceTypePack serviceTypePack { get; set; }
        public long HospitalServiceTypePackId { get; set; }

        public ServiceType serviceType { get; set; }
        public long ServiceTypeId { get; set; }

        public Authorization authorization { get; set; }
        public long? AuthorizationId { get; set; }
    }
}
