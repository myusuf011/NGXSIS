namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_riwayat_proyek
    {
        public long id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        public long riwayat_pekerjaan_id { get; set; }

        [StringLength(10)]
        public string start_year { get; set; }

        [StringLength(10)]
        public string start_month { get; set; }

        [StringLength(50)]
        public string poject_name { get; set; }

        public int? project_duration { get; set; }

        public long? time_period_id { get; set; }

        [StringLength(100)]
        public string client { get; set; }

        [StringLength(100)]
        public string project_position { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        public virtual x_riwayat_pekerjaan x_riwayat_pekerjaan { get; set; }

        public virtual x_time_period x_time_period { get; set; }
    }
}
