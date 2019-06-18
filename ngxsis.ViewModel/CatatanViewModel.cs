using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ngxsis.ViewModel
{
    public class CatatanViewModel
    { 
    public long id { get; set; }

    public long biodata_id { get; set; }
        [Display(Name = "Judul Catatan*")]
        [Required(ErrorMessage = "Judul Catatan harus diisi")]
        public string title { get; set; }
        [Display(Name = "Jenis Catatan*")]
        [Required(ErrorMessage = "Jenis Catatan harus diisi")]
        public long? note_type_id { get; set; }
       // public long userid { get; set; }

        public string note_type_name { get; set; }

        public string biodata_name { get; set; }
        [Display(Name = "Catatan")]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }
        public long user_id { get; set; }

}
}
