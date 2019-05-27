namespace ngxsis.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class x_menutree
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public x_menutree()
        {
            x_menu_acces = new HashSet<x_menu_acces>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [StringLength(100)]
        public string menu_image_url { get; set; }

        [StringLength(100)]
        public string menu_icon { get; set; }

        public int menu_order { get; set; }

        public int menu_level { get; set; }

        public long? menu_parent { get; set; }

        [Required]
        [StringLength(100)]
        public string menu_url { get; set; }

        [Required]
        [StringLength(10)]
        public string menu_type { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<x_menu_acces> x_menu_acces { get; set; }
    }
}
