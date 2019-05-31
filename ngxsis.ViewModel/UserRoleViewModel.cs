using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class UserRoleViewModel
    {
        public long id
        {
            get; set;
        }

        [Required(ErrorMessage = "E-mail tidak boleh kosong")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Format email salah contoh : anonymous@mail.com")]
        [Display(Name ="Username *")]
        public string Email
        {
            get; set;
        }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Sandi *")]
        public string Abpwd
        {
            get; set;
        }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Abpwd",ErrorMessage = "Sandi yang dimasukkan tidak sama")]
        [Display(Name = "Ulang Sandi *")]
        public string ConfirmAbpwd
        {
            get; set;
        }

        public long RoleId
        {
            get; set;
        }
        public long? AddrBookId
        {
            get; set;
        }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nama Lengkap")]
        public string FullName
        {
            get; set;
        }
    }
}
