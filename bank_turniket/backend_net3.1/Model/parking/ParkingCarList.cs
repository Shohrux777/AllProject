using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.parking
{
    [Table("parking_car_list")]
    public class ParkingCarList : ParkingBase
    {
        public String type { get; set; }
        public String car_plate { get; set; }
        public String image_name  { get; set; }
        public DateTime created_date_time { get; set; } = DateTime.Now;
        
    }
}
