using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ngxsis.ViewModel
{
    // Bagian Pelamar Terjadwal
    public class PelamarTerjadwalViewModel
    {
        // dari tabel x_rencana_jadwal
        public long id { get; set; }

        public long create_by { get; set; }

        public DateTime create_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? delete_by { get; set; }

        public DateTime? delete_on { get; set; }

        public bool is_delete { get; set; }

        public string schedule_code { get; set; }

        [Required(ErrorMessage = "Tanggal Rencana Jadwal harus diisi")]
        [Remote("IsScheduleDateValid", "Penjadwalan", ErrorMessage = "Tanggal Rencana Jadwal harus lebih besar dari hari ini")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? schedule_date { get; set; }

        public string schedule_date_string { get; set; }

        [Required(ErrorMessage = "Jam harus diisi")]
        public string time { get; set; }

        [Remote("IsROValid", "Penjadwalan", AdditionalFields = "id, schedule_date, time", ErrorMessage = "RO sudah terjadwal pada tanggal dan jam tersebut")]
        public long? ro { get; set; }

        public string ro_name { get; set; }

        [Remote("IsTROValid", "Penjadwalan", AdditionalFields = "id, schedule_date, time", ErrorMessage = "TRO sudah terjadwal pada tanggal dan jam tersebut")]
        public long? tro { get; set; }

        public string tro_name { get; set; }

        [Required(ErrorMessage = "Jenis Jadwal harus diisi")]
        public long? schedule_type_id { get; set; }

        public string schedule_type_name { get; set; }

        public string location { get; set; }

        public string other_ro_tro { get; set; }

        public string notes { get; set; }

        [Display(Name = "Kirim Undangan Otomatis")]
        public bool? is_automatic_mail { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? sent_date { get; set; }

        public string sent_date_string { get; set; }

        public string status { get; set; }

        // lain-lain yang dibutuhkan

        public long user_id { get; set; }

        public long biodata_id { get; set; }

        public string fullname { get; set; }

        public long rencana_jadwal_detail_id { get; set; }

        public List<Pelamar> pelamar { get; set; }
    }

    public class Pelamar
    {
        public long rjd_id { get; set; }

        public long biodata_id { get; set; }

        public string fullname { get; set; }

        public string school_name { get; set; }

        public string major { get; set; }

        public string email { get; set; }
    }
}
