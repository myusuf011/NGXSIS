namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_biodata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public x_biodata()
        {
            x_address = new HashSet<x_address>();
            x_biodata_attachment = new HashSet<x_biodata_attachment>();
            x_catatan = new HashSet<x_catatan>();
            x_keahlian = new HashSet<x_keahlian>();
            x_keluarga = new HashSet<x_keluarga>();
            x_keterangan_tambahan = new HashSet<x_keterangan_tambahan>();
            x_online_test = new HashSet<x_online_test>();
            x_organisasi = new HashSet<x_organisasi>();
            x_pe_referensi = new HashSet<x_pe_referensi>();
            x_rencana_jadwal_detail = new HashSet<x_rencana_jadwal_detail>();
            x_riwayat_pekerjaan = new HashSet<x_riwayat_pekerjaan>();
            x_riwayat_pelatihan = new HashSet<x_riwayat_pelatihan>();
            x_riwayat_pendidikan = new HashSet<x_riwayat_pendidikan>();
            x_sertifikasi = new HashSet<x_sertifikasi>();
            x_sumber_loker = new HashSet<x_sumber_loker>();
            x_undangan_detail = new HashSet<x_undangan_detail>();
        }

        public long id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        [Required]
        [StringLength(255)]
        public string fullname { get; set; }

        [Required]
        [StringLength(100)]
        public string nick_name { get; set; }

        [Required]
        [StringLength(100)]
        public string pob { get; set; }

        [Column(TypeName = "date")]
        public DateTime dob { get; set; }

        public bool gender { get; set; }

        public long religion_id { get; set; }

        public int? high { get; set; }

        public int? weight { get; set; }

        [StringLength(100)]
        public string nationality { get; set; }

        [StringLength(50)]
        public string ethnic { get; set; }

        [StringLength(25)]
        public string hobby { get; set; }

        public long identity_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string identity_no { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string phone_number1 { get; set; }

        [StringLength(50)]
        public string phone_number2 { get; set; }

        [Required]
        [StringLength(50)]
        public string parent_phone_number { get; set; }

        [StringLength(5)]
        public string child_sequence { get; set; }

        [StringLength(5)]
        public string how_many_brothers { get; set; }

        public long marital_status_id { get; set; }

        public long? addrbook_id { get; set; }

        [StringLength(10)]
        public string token { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expired_token { get; set; }

        [StringLength(50)]
        public string marriage_year { get; set; }

        public long company_id { get; set; }

        public bool? is_process { get; set; }

        public bool? is_complete { get; set; }

        public virtual x_addrbook x_addrbook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_address> x_address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_biodata_attachment> x_biodata_attachment { get; set; }

        public virtual x_company x_company { get; set; }

        public virtual x_identity_type x_identity_type { get; set; }

        public virtual x_marital_status x_marital_status { get; set; }

        public virtual x_religion x_religion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_catatan> x_catatan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_keahlian> x_keahlian { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_keluarga> x_keluarga { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_keterangan_tambahan> x_keterangan_tambahan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_online_test> x_online_test { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_organisasi> x_organisasi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_pe_referensi> x_pe_referensi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_rencana_jadwal_detail> x_rencana_jadwal_detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_riwayat_pekerjaan> x_riwayat_pekerjaan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_riwayat_pelatihan> x_riwayat_pelatihan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_riwayat_pendidikan> x_riwayat_pendidikan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_sertifikasi> x_sertifikasi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_sumber_loker> x_sumber_loker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_undangan_detail> x_undangan_detail { get; set; }
    }
}
