using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;


namespace ngxsis.ViewModel
{
    public class PendidikanViewModel
    {
        public long id { get; set; }     

        public long biodata_id { get; set; }

        [Display(Name = "Nama Sekolah/Instansi*")]
        [Required(ErrorMessage = "Nama Sekolah/Instansi harus diisi")]
        [StringLength(100)]
        public string school_name { get; set; }
        [Display(Name = "Kota")]
        [StringLength(50)]
        public string city { get; set; }
        [Display(Name = "Negara")]
        [StringLength(50)]
        public string country { get; set; }
        [Display(Name = "Jenjang Pendidikan*")]
       

        public string educationName { get; set; }
        [Display(Name = "Jenjang Pendidikan*")]
        [Required(ErrorMessage ="Jenjang Pendidikan harus diisi")]
        public long? education_level_id { get; set; }
        [Remote("jenjang", "Pendidikan", AdditionalFields = "education_level_id", ErrorMessage = "jenjang pendidikan tidak boleh sama")]
        [Display(Name = "Tahun Masuk")]
        [StringLength(10)]
  
        public string entry_year { get; set; }
        [Display(Name = "Tahun Lulus")]
        [Remote("IsGradYearValid", "Pendidikan", AdditionalFields = "entry_year", ErrorMessage = "Tahun lulus tidak boleh kurang dari tahun masuk")]
        [StringLength(10)]
        public string graduation_year { get; set; }
        [Display(Name = "Jurusan")]
        [StringLength(100)]
        public string major { get; set; }
        [Display(Name = "IPK")] 
        public double? gpa { get; set; }
        [Display(Name = "Catatan")]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }

        public long user_id { get; set; }



    }
}
