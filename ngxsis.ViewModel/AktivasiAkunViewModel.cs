using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class AktivasiAkunViewModel
    {
        public long online_test_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        public long? biodata_id { get; set; }

        [StringLength(500)]
        public string period_code { get; set; }

        public int? period { get; set; }

        [Column(TypeName = "date")]
        public DateTime? test_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expired_test { get; set; }

        [StringLength(10)]
        public string user_access { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public long? addrbook_id { get; set; }

        public bool is_delete_akun { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string abuid { get; set; }

        [Required]
        [StringLength(50)]
        public string abpwd { get; set; }

        public long online_test_detail_id { get; set; }

        public int? test_order { get; set; }

        [Display(Name = "Jenis Tes")]
        public long? test_type_id { get; set; }

        [StringLength(50)]
        public string name_type { get; set; }
    }
}
