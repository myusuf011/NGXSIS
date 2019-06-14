namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_keluarga
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

        public long? family_tree_type_id { get; set; }

        public long? family_relation_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public bool gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dob { get; set; }

        public long? education_level_id { get; set; }

        [StringLength(100)]
        public string job { get; set; }

        [StringLength(1000)]
        public string notes { get; set; }

        public virtual x_biodata x_biodata { get; set; }

        public virtual x_education_level x_education_level { get; set; }

        public virtual x_family_tree_type x_family_tree_type { get; set; }

        public virtual x_family_tree_type x_family_tree_type1 { get; set; }
    }
}
