using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;


namespace ngxsis.ViewModel
{
    public class KeluargaViewModel
    {
        public long id { get; set; }

        public long biodata_id { get; set; }

        [Display(Name = "Jenis Susunan Keluarga*")]
        [Required(ErrorMessage = "Jenis Susunan Keluarga harus diisi")]
        public long? family_tree_type_id { get; set; }
        [Display(Name = "Hubungan Keluarga*")]
        [Required(ErrorMessage = "Hubungan Keluarga harus diisi")]
        public long? family_relation_id { get; set; }

        [Display(Name = "Nama*")]
        [Required(ErrorMessage = "Nama harus diisi")]
        [StringLength(100)]
        public string name { get; set; }
        [Display(Name = "Jenis Kelamin")]
        public bool gender { get; set; }

        //[DataType(DataType.Date)]
        // [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  //tampil di list

        [Display(Name = "Tgl. Lahir (yyyy-mm-dd)")]
        public DateTime? dob { get; set; }
        [Display(Name = "Pendidikan")]
        public long? education_level_id { get; set; }
        [Display(Name = "Pekerjaan")]
        [StringLength(100)]
        public string job { get; set; }
        [Display(Name = "Catatan")]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }

        public string family_relation_name { get; set; }

        public string education_level_name { get; set; }

        public string family_tree_type_name { get; set; }
        public long user_id { get; set; }


    }
}