namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_address
    {
        public long id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        public long biodata_id { get; set; }

        [StringLength(1000)]
        public string address1 { get; set; }

        [StringLength(20)]
        public string postal_code1 { get; set; }

        [StringLength(5)]
        public string rt1 { get; set; }

        [StringLength(5)]
        public string rw1 { get; set; }

        [StringLength(100)]
        public string kelurahan1 { get; set; }

        [StringLength(100)]
        public string kecamatan1 { get; set; }

        [StringLength(100)]
        public string region1 { get; set; }

        [StringLength(1000)]
        public string address2 { get; set; }

        [StringLength(20)]
        public string postal_code2 { get; set; }

        [StringLength(5)]
        public string rt2 { get; set; }

        [StringLength(5)]
        public string rw2 { get; set; }

        [StringLength(100)]
        public string kelurahan2 { get; set; }

        [StringLength(100)]
        public string kecamatan2 { get; set; }

        [StringLength(100)]
        public string region2 { get; set; }

        public virtual x_biodata x_biodata { get; set; }
    }
}
