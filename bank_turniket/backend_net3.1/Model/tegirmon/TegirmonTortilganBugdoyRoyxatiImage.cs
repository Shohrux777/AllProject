using ApiAll.Controllers.tegirmon;
using System;

namespace ApiAll.Model.tegirmon
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tegirmon_tortilgan_bugdoy_royxati_image")]
    public class TegirmonTortilganBugdoyRoyxatiImage : TegirmonBaseModel
    {
        public long? TegirmonTortilganBugdoyRoyxatiGroupDetailid { get; set; }
        public TegirmonTortilganBugdoyRoyxatiGroupDetail group_detail { get; set; }
        public long? TegirmonTortilganBugdoyRoyxatiid { get; set; }
        public TegirmonTortilganBugdoyRoyxati royxati { get; set; }
        public String image_url { get; set; }
        public String image_url_2 { get; set; }
    }
}
