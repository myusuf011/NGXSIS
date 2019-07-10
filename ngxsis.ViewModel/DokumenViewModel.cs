using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ngxsis.ViewModel
{
    public class Bio
    {
        public long id { get; set; }
        public List<DokumenViewModel> dokumen { get; set; }
    }
    public class DokumenViewModel
    {
        public long id { get; set; }

        public bool is_delete { get; set; }

        public long biodata_id { get; set; }

        [StringLength(100), Display(Name = "Nama File *")]
        [Required(ErrorMessage = "Nama file harus di isi.")]
        public string file_name { get; set; }

        [StringLength(1000)]
        public string file_path { get; set; }

        [StringLength(1000),Display(Name = "Keterangan")]
        public string notes { get; set; }

        public bool? is_photo { get; set; }
        public HttpPostedFileBase file { get; set; }
        public HttpPostedFileBase foto { get; set; }
    }
}
