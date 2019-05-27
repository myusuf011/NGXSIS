namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_rencana_jadwal_detail
    {
        public long id { get; set; }

        public long create_by { get; set; }

        public DateTime create_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? delete_by { get; set; }

        public DateTime? delete_on { get; set; }

        public bool is_delete { get; set; }

        public long rencana_jadwal_id { get; set; }

        public long biodata_id { get; set; }

        public virtual x_biodata x_biodata { get; set; }

        public virtual x_rencana_jadwal x_rencana_jadwal { get; set; }
    }
}
