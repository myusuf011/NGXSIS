namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_organisasi
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
        public string name { get; set; }

        [StringLength(100)]
        public string position { get; set; }

        [StringLength(10)]
        public string entry_year { get; set; }

        [StringLength(10)]
        public string exit_year { get; set; }

        [StringLength(100)]
        public string responsibility { get; set; }

        [StringLength(1000)]
        public string notes { get; set; }

        public virtual x_biodata x_biodata { get; set; }
    }
}
