namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_riwayat_pekerjaan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public x_riwayat_pekerjaan()
        {
            x_riwayat_proyek = new HashSet<x_riwayat_proyek>();
        }

        public long id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        public long biodata_id { get; set; }

        [StringLength(500)]
        public string company_name { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(50)]
        public string country { get; set; }

        [StringLength(10)]
        public string join_year { get; set; }

        [StringLength(10)]
        public string join_month { get; set; }

        [StringLength(10)]
        public string resign_year { get; set; }

        [StringLength(10)]
        public string resign_month { get; set; }

        [StringLength(100)]
        public string last_position { get; set; }

        [StringLength(20)]
        public string income { get; set; }

        public bool? is_it_related { get; set; }

        [StringLength(1000)]
        public string about_job { get; set; }

        [StringLength(500)]
        public string exit_reason { get; set; }

        [StringLength(5000)]
        public string notes { get; set; }

        public virtual x_biodata x_biodata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_riwayat_proyek> x_riwayat_proyek { get; set; }
    }
}
