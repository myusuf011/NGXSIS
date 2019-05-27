using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ngxsis.ViewModel
{
    public class LoginViewModel
    {
        public long id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        public bool is_locked { get; set; }

        [Required(ErrorMessage = "Kolom harus diisi")]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string abuid { get; set; }

        [Required(ErrorMessage = "Kolom harus diisi")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string abpwd { get; set; }
    }
}
