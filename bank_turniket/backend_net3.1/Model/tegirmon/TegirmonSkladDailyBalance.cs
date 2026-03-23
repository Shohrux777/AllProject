using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("tegirmon_sklad_daily_balance")]
public class TegirmonSkladDailyBalance : TegirmonBaseModel
{
    public DateTime Date { get; set; } // Kun
    public long TegirmonSkladId { get; set; }
    public TegirmonSklad Sklad { get; set; }
    public long TegirmonProductId { get; set; }
    public TegirmonProduct Product { get; set; }
    public double qty { get; set; } // Shu kunda qolgan qoldiq
}
}
