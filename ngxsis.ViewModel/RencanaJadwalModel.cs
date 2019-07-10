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
    public class RencanaJadwalModel
    {
        public long id { get; set; }

        public long create_by { get; set; }

        public DateTime create_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? delete_by { get; set; }

        public DateTime? delete_on { get; set; }

        public bool is_delete { get; set; }

        [StringLength(20)]
        public string schedule_code { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Tgl. Rencana Jadwal! ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yy}", ApplyFormatInEditMode = true)]
        public DateTime? schedule_date { get; set; }


        [StringLength(10)]
        public string time { get; set; }
        [Required(ErrorMessage = "Silahkan Isi Kolom Jam Terlebih Dahulu! ")]
        [Remote("VerifyDate", "Penjadwalan", AdditionalFields = "time,schedule_date,id", ErrorMessage = "RO Sudah Terjadwal, Silahkan Pilih Waktu yang Berbeda!")]
        public long? ro { get; set; }

        [Remote("Verifytro", "Penjadwalan", AdditionalFields = "time,schedule_date,id", ErrorMessage = "TRO Sudah Terjadwal, Silahkan Pilih Waktu yang Berbeda!")]

        public long? tro { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Jenis Jadwal Terlebih Dahulu! ")]
        public long? scedule_type_id { get; set; }

        [StringLength(100)]
        public string location { get; set; }

        [StringLength(100)]
        public string other_ro_tro { get; set; }

        [StringLength(1000)]
        public string notes { get; set; }

        public bool? is_automatic_mail { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yy}", ApplyFormatInEditMode = true)]

        [Required(ErrorMessage = "Silahkan Isi Kolom Tanggal! ")]
        public DateTime? sent_date { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public string scedule_type_name { get; set; }
        public string ro_name { get; set; }
        public string tro_name { get; set; }
        public string date_name { get; set; }

        public long user_id { get; set; }

        public List<DropDownModel> pelamar_list { get; set; }

    }
}
