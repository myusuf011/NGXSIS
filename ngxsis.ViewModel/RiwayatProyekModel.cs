using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ngxsis.ViewModel
{
    public class RiwayatProyekModel
    {


        public long id { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_deleted { get; set; }

        public long riwayat_pekerjaan_id { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Tahun Mulai Proyek Terlebih Dahulu!")]
        [StringLength(10)]
        public string start_year { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Bulan Mulai Proyek Terlebih Dahulu!")]
        [Display(Name = "Waktu Mulai Proyek *")]
        [StringLength(10)]
        public string start_month { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Nama Proyek Terlebih Dahulu!")]
        [Display(Name = "Nama Proyek *")]
        [StringLength(50)]
        public string poject_name { get; set; }

        [Display(Name = "Lama Pengerjaan")]
        public int? project_duration { get; set; }

        public long? time_period_id { get; set; }

        [Display(Name = "Klien")]
        [StringLength(100)]
        public string client { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Posisi Ketika Proyek Terlebih Dahulu!")]
        [Display(Name = "Posisi Ketika Proyek *")]
        [StringLength(100)]
        public string project_position { get; set; }

        [Display(Name = "Keterangan")]
        [StringLength(1000)]
        public string description { get; set; }

        [StringLength(10)]
        public string periodname { get; set; }
        public long user_id { get; set; }
    }
}
