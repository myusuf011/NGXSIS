using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class KeahlianViewModel
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
        [Display(Name = "Nama Keahlian")]
        public string skill_name { get; set; }

        [Display(Name = "Level Keahlian *")]
        [Required(ErrorMessage = "Level keahlian harus diisi")]
        public long? skill_level_id { get; set; }

        [StringLength(1000)]
        [Display(Name = "Catatan")]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }

        public string skill_level_name { get; set; }

        public long user_id { get; set; }

        //public virtual x_biodata x_biodata { get; set; }

        //public virtual x_skill_level x_skill_level { get; set; }
    }
}

