using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAll.Model.tegirmon
{
    [Table("analiz_hos_blank")]
    public class AnalizHosBlank : TegirmonBaseModel
    {
        public String name { get; set; }
        public String title { get; set; }
        public String code { get; set; }
        public String note { get; set; }
        public String image_url { get; set; }
        public String string_1 { get; set; }
        public String string_2 { get; set; }
        public long? top_height { get; set; } = 0;
        public long? shtrex_top { get; set; } = 0;
        public long? shtrex_left { get; set; } = 0;
        public long? shtrex_right { get; set; } = 0;
        public long? shtrex_bottom { get; set; } = 0;
        public long? num_1 { get; set; } = 0;
        public long? num_2 { get; set; } = 0;
    }
}