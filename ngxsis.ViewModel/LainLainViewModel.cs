using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class LainLainViewModel
    {
        public long biodata_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }
                
        public long referensiId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nama *")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string name { get; set; }

        [StringLength(100)]
        [Display(Name = "Jabatan")]
        public string position { get; set; }

        [StringLength(1000)]
        [Display(Name = "Alamat & No. HP")]
        public string address_phone { get; set; }

        [StringLength(100)]
        [Display(Name = "Hubungan")]
        public string relation { get; set; }
    }
}
