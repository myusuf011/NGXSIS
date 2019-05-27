using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class RoleViewModel
    {
        public long Id
        {
            get; set;
        }

        [Required]
        [StringLength(50, ErrorMessage = "Kode tidak boleh lebih dari 50 karakter")]
        [Display(Name = "KODE ROLE")]
        public string Code
        {
            get; set;
        }

        [Required]
        [StringLength(50, ErrorMessage = "Nama tidak boleh lebih dari 50 karakter")]
        [Display(Name = "NAMA ROLE")]
        public string Name
        {
            get; set;
        }

        public long LoginUserId
        {
            get; set;
        }

        public bool IsDeleted
        {
            get; set;
        }
    }
}
