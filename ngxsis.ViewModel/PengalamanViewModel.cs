using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class PengalamanViewModel
    {
        public long id { get; set; }

        public long biodata_id { get; set; }

        [StringLength(500)]
        [Display(Name = "Nama Perusahaan/Instansi *")]
        [Required(ErrorMessage = "Nama Perusahaan/Instansi harus diisi")]
        public string company_name { get; set; }

        [StringLength(50)]
        [Display(Name = "Kota")]
        public string city { get; set; }

        [StringLength(50)]
        [Display(Name = "Negara")]
        public string country { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Tahun Masuk harus diisi")]
        public string join_year { get; set; }

        [StringLength(10)]
        [Display(Name = "Waktu Masuk *")]
        [Required(ErrorMessage = "Bulan Masuk harus diisi")]
        public string join_month { get; set; }

        [StringLength(10)]
        public string resign_year { get; set; }

        [StringLength(10)]
        [Display(Name = "Waktu Keluar")]
        public string resign_month { get; set; }

        [StringLength(100)]
        [Display(Name = "Posisi Terakhir *")]
        [Required(ErrorMessage = "Posisi Terakhir harus diisi")]
        public string last_position { get; set; }

        [StringLength(20)]
        [Display(Name = "Penghasilan Terakhir (IDR)")]
        public string income { get; set; }

        [Display(Name = "IT Related?")]
        public bool? is_it_related { get; set; }

        [StringLength(1000)]
        [Display(Name = "Keterangan Singkat Mengenai Pekerjaan")]
        public string about_job { get; set; }

        [StringLength(500)]
        [Display(Name = "Alasan Keluar")]
        public string exit_reason { get; set; }

        [StringLength(5000)]
        [Display(Name = "Catatan")]
        public string notes { get; set; }
    }
}

