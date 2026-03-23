using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.pos
{
    [Table("pos_check")]
    public class PosCheck:PosBaseModel
    {
        public String reserved_check_1 { get; set; }
        public String reserved_check_2 { get; set; }
        public String reserved_check_3 { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public double summ { get; set; } = 0.0;
        public double card_sum { get; set; } = 0.0;
        public double cash_sum { get; set; } = 0.0;
        public double payme_sum { get; set; } = 0.0;
        public double click_sum { get; set; } = 0.0;
        public double online_sum { get; set; } = 0.0;
        public double humo_sum { get; set; } = 0.0;
        public double mobil_sum { get; set; } = 0.0;
        public double discount_summ { get; set; } = 0.0;
        public double bonus_summ { get; set; } = 0.0;
        public double profit_summ { get; set; } = 0.0;
        public long? pos_client_dog_id { get; set; }
        public List<PosPayments> paymentsList { get; set; }
        public bool check_revert_status { get; set; } = false;
        public bool closed_status { get; set; } = false;

        public long? pos_client_id { get; set; } = 0;

        public long PosAuthorizationid { get; set; }
        public PosAuthorization auth { get; set; }

        [NotMapped]
        public PosClient client { get; set; }
        [NotMapped]
        public String client_name_str => client != null ? client.fio : "";

    }
}
