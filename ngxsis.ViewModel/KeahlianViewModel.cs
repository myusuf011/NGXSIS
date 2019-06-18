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
    public class KeahlianViewModel
    {
        public long id { get; set; }
        
        public long biodata_id { get; set; }

        [StringLength(100)]
        [Display(Name = "Nama Keahlian")]
        public string skill_name { get; set; }

        [Display(Name = "Level Keahlian *")]
        [Required(ErrorMessage = "Level keahlian harus diisi")]
        //[Remote("IsSkillLevelExist", "Keahlian", AdditionalFields = "id, biodata_id", ErrorMessage = "Level keahlian sudah digunakan")]
        public long? skill_level_id { get; set; }

        [StringLength(1000)]
        [Display(Name = "Catatan")]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }

        public string skill_level_name { get; set; }

        public long user_id { get; set; }

    }
}

