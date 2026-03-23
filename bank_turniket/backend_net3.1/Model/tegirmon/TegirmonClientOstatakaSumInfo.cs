using System;
namespace ApiAll.Model.tegirmon
{
    public class TegirmonClientOstatakaSumInfo
    {
        public long TegirmonProductid { get; set; } 
        public String name { get; set; }
        public double? qty { get; set; } = 0.0;
        public double? real_qty { get; set; } = 0.0;

    }
}
