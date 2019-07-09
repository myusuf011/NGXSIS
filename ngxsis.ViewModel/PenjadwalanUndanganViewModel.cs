using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ngxsis.ViewModel
{
    public class PenjadwalanUndanganViewModel
    {

        public long id { get; set; }


        [Required(ErrorMessage = "Jenis Undangan harus diisi")]
        public long? schedule_type_id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  //tampil di list
        [Required(ErrorMessage = "Tgl.Undangan harus diisi")]
        [Remote("IsTanggalUndanganValid", "PenjadwalanUndangan", ErrorMessage = "Tanggal Undangan harus lebih dari hari ini")]
        public DateTime? invitation_date { get; set; }
        

        [StringLength(20)]
        public string invitation_code { get; set; }

        [Required(ErrorMessage = "Jam harus diisi")]
        [StringLength(10)]
        public string time { get; set; }
      //  [Remote("IsValidRO", "PenjadwalanUndangan", AdditionalFields = "id, invitation_date, time", ErrorMessage = "RO sudah terjadwal pada tanggal dan jam tersebut")]
        public long? ro { get; set; }

      //  [Remote("IsValidTRO", "PenjadwalanUndangan", AdditionalFields = "id, invitation_date, time", ErrorMessage = "TRO sudah terjadwal pada tanggal dan jam tersebut")]
        public long? tro { get; set; }

        [StringLength(100)]
        public string other_ro_tro { get; set; }

        [StringLength(100)]
        public string location { get; set; }

        [StringLength(50)]
        public string status { get; set; }
        [Required(ErrorMessage = "Pelamar harus diisi")]
        public long biodata_id { get; set; }
        public long user_id { get; set; }

        public string Fullname { get; set; }

        public string SchoolName { get; set; }

        public string Major { get; set; }

       
        public string schedule_type_name { get; set; }


       

        public string notes { get; set; }

        public string ro_name { get; set; }

        public string tro_name { get; set; }

        public string date_string { get; set; }

    }
}
