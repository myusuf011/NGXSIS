using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class PelatihanViewModel
    {
        public long id { get; set; }

        public long biodata_id { get; set; }

        [StringLength(100)]
        [Display(Name = "Nama (kursus, penataran, pelatihan, workshop) *")]
        [Required(ErrorMessage = "Nama Pelatihan harus diisi")]
        public string training_name { get; set; }

        [StringLength(50)]
        [Display(Name = "Penyelenggara *")]
        [Required(ErrorMessage = "Nama Penyelenggara harus diisi")]
        public string organizer { get; set; }

        [StringLength(10)]
        public string training_year { get; set; }

        [StringLength(10)]
        [Display(Name = "Waktu Pelaksanaan")]
        public string training_month { get; set; }

        [Display(Name = "Lama Durasi")]
        public int? training_duration { get; set; }

        public long? time_period_id { get; set; }

        [StringLength(50)]
        [Display(Name = "Kota")]
        public string city { get; set; }

        [StringLength(50)]
        [Display(Name = "Negara")]
        public string country { get; set; }

        [StringLength(1000)]
        [Display(Name = "Catatan")]
        public string notes { get; set; }

        public string time_period_name { get; set; }
    }
}