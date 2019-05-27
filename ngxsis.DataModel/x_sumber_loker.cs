namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_sumber_loker
    {
        public long id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        public long biodata_id { get; set; }

        [StringLength(20)]
        public string vacancy_source { get; set; }

        [StringLength(10)]
        public string candidate_type { get; set; }

        [StringLength(100)]
        public string event_name { get; set; }

        [StringLength(100)]
        public string career_center_name { get; set; }

        [StringLength(100)]
        public string referrer_name { get; set; }

        [StringLength(20)]
        public string referrer_phone { get; set; }

        [StringLength(100)]
        public string referrer_email { get; set; }

        [StringLength(100)]
        public string other_source { get; set; }

        [StringLength(20)]
        public string last_income { get; set; }

        [Column(TypeName = "date")]
        public DateTime? apply_date { get; set; }

        public long? is_process { get; set; }

        public virtual x_biodata x_biodata { get; set; }
    }
}
