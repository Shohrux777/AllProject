using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAll.Model.skud
{
   
    public class SkudBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [JsonProperty("allowUploading", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(true)]
        public bool? active_status { get; set; } = true;
        public DateTime? created_date_time { get; set; } = DateTime.Now;
        public DateTime? updated_date_time { get; set; } = DateTime.Now;

    }
}
