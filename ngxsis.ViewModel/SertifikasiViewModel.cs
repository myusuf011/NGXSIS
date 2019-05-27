using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ngxsis.ViewModel
{
    public class SertifikasiViewModel
    {
        public long id { get; set; }

        [StringLength(200)]
        [Display(Name = "Nama Sertifikasi")]
        public string certificate_name { get; set; }
        [Display(Name = "Penerbit")]

        [StringLength(100)]

        public string publisher { get; set; }

        [Display(Name = "Berlaku Mulai")]

        [StringLength(10)]
        public string valid_start_year { get; set; }

        [StringLength(10)]
        public string valid_start_month { get; set; }
        [Display(Name = "Berlaku Sampai")]

        [StringLength(10)]
        public string until_year { get; set; }

        [StringLength(10)]
        public string until_month { get; set; }

        [StringLength(1000)]
        public string notes { get; set; }
        public long biodata_id { get; set; }



    }
}