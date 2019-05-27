namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_online_test_detail
    {
        public long id { get; set; }

        public long created_by { get; set; }

        public DateTime create_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? delete_by { get; set; }

        public DateTime? delete_on { get; set; }

        public bool is_delete { get; set; }

        public long? online_test_id { get; set; }

        public long? test_type_id { get; set; }

        public int? test_order { get; set; }

        public virtual x_online_test x_online_test { get; set; }

        public virtual x_test_type x_test_type { get; set; }
    }
}
