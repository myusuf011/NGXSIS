using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class SumberLokerViewModel
    {
        public long id { get; set; }
        public long created_by { get; set; }
        public DateTime created_on { get; set; }
        public long? modified_by { get; set; }
        public DateTime? modified_on { get; set; }
        public long? deleted_by { get; set; }
        public DateTime? deleted_on { get; set; }
        public bool is_delete { get; set; }
        public long biodata_id { get; set; }

        [DisplayName("Sumber"), StringLength(20)]
        public string vacancy_source { get; set; }

        [DisplayName("Tipe Pelamar"), StringLength(10)]
        public string candidate_type { get; set; }

        [Required(ErrorMessage = "Please enter Nama Event")]
        [DisplayName("Nama Event *"), StringLength(100)]
        public string event_name { get; set; }

        [Required(ErrorMessage = "Please enter Karir Center")]
        [DisplayName("Nama Karir Center *"), StringLength(100)]
        public string career_center_name { get; set; }

        [Required(ErrorMessage = "Please enter Referrer Name")]
        [DisplayName("Referrer Name *"), StringLength(100)]
        public string referrer_name { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [DisplayName("Referrer Mobile Number *"), StringLength(20)]
        public string referrer_phone { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")]
        [Required(ErrorMessage = "E-mail is not valid")]
        [DisplayName("Referrer Email *"), StringLength(100)]
        public string referrer_email { get; set; }

        [Required(ErrorMessage = "Sumber Lain Tidak Boleh Kosong")]
        [DisplayName("Sumber Lain *"), StringLength(100)]
        public string other_source { get; set; }

        [DisplayName("Penghasilan Terakhir (IDR)"), StringLength(100)]
        public string last_income { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Tgl. Lamaran Kerja (yyyy-mm-dd)")]
        public DateTime? apply_date { get; set; }

        public long? is_process { get; set; }

    }
}
