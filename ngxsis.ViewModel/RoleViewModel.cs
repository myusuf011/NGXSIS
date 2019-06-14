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
    public class RoleViewModel
    {
   
        public long Id
        {
            get; set;
        }

        [Required(ErrorMessage = "Kode role tidak boleh kosong")]
        [StringLength(50, ErrorMessage = "Kode tidak boleh lebih dari 50 karakter")]
        [Display(Name = "KODE ROLE")]
        [Remote("IsCodeUnique","Role",AdditionalFields = "Id",ErrorMessage = "Kode role sudah digunakan!")]
        public string Code
        {
            get; set;
        }

        [Required(ErrorMessage = "Nama role tidak boleh kosong")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Nama role hanya boleh mengandung huruf")]
        [StringLength(50, ErrorMessage = "Nama tidak boleh lebih dari 50 karakter")]
        [Display(Name = "NAMA ROLE")]
        [Remote("IsNameUnique","Role",AdditionalFields = "Id",ErrorMessage = "Nama role sudah digunakan!")]
        public string Name
        {
            get; set;
        }

        public long LoginUserId
        {
            get; set;
        }
    }
}
