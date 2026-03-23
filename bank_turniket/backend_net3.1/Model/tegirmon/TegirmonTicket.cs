using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_ticket")]
    public class TegirmonTicket : TegirmonBaseModel
    {
        public String ticket_number { get; set; }
        public double massa { get; set; } = 0;
        public String organization_name { get; set; }
        public long? tegirmonHisoblar_id { get; set; }
        public String note { get; set; }
        public String hisob_name { get; set; }
        public String address { get; set; }
        public DateTime? ticket_date { get; set; }
        public double price { get; set; } = 0.0;
        public double total_price { get; set; } = 0.0;
        public long? tegirmonAuthId { get; set; }
        public bool is_closed { get; set; } = false;
        public DateTime? closed_date_time { get; set; }
        public double percentage { get; set; } = 0.0;
        public double invoice_massa { get; set; } = 0.0;
    }
}

