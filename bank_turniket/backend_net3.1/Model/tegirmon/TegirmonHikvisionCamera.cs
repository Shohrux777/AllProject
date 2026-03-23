using System;
namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_hikvision_camera")]
    public class TegirmonHikvisionCamera : TegirmonBaseModel
    {
        public String name { get; set; }
        public String ip { get; set; }
        public String username { get; set; }
        public String password { get; set; }
    }
}
