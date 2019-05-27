namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_riwayat_pelatihan
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

        [StringLength(100)]
        public string training_name { get; set; }

        [StringLength(50)]
        public string organizer { get; set; }

        [StringLength(10)]
        public string training_year { get; set; }

        [StringLength(10)]
        public string training_month { get; set; }

        public int? training_duration { get; set; }

        public long? time_period_id { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(50)]
        public string country { get; set; }

        [StringLength(1000)]
        public string notes { get; set; }

        public virtual x_biodata x_biodata { get; set; }

        public virtual x_time_period x_time_period { get; set; }
    }
}
