using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class OrganisasiViewModel
    {
        public long id { get; set; }

        //public long created_by { get; set; }

        //public DateTime created_on { get; set; }

        //public long? modified_by { get; set; }

        //public DateTime? modified_on { get; set; }

        //public long? deleted_by { get; set; }

        //public DateTime? deleted_on { get; set; }

        //public bool is_delete { get; set; }

        public long biodata_id { get; set; }

        [StringLength(100)]
        [Display(Name = "Nama Organisasi *")]
        [Required(ErrorMessage = "Nama organisasi harus diisi")]
        public string name { get; set; }

        [StringLength(100)]
        [Display(Name = "Jabatan *")]
        [Required(ErrorMessage = "Jabatan harus diisi")]
        public string position { get; set; }

        [StringLength(10)]
        [Display(Name = "Tahun Masuk *")]
        [Required(ErrorMessage = "Tahun masuk harus diisi")]
        public string entry_year { get; set; }

        [StringLength(10)]
        [Display(Name = "Tahun Keluar *")]
        [Required(ErrorMessage = "Tahun keluar harus diisi")]
        public string exit_year { get; set; }

        [StringLength(100)]
        [Display(Name = "Tanggung Jawab")]
        public string responsibility { get; set; }

        [StringLength(1000)]
        [Display(Name = "Catatan")]
        public string notes { get; set; }
    }
}
