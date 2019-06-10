using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        [Required(ErrorMessage = "Sandi role tidak boleh kosong")]
        [DataType(DataType.Password)]
        [Display(Name ="Sandi *")]
        public string Abpwd
        {
            get; set;
        }
        [Required(ErrorMessage = "Sandi role tidak boleh kosong")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Abpwd")]
        [Display(Name = "Ulang Sandi *")]
        public string ConfirmAbpwd
        {
            get; set;
        }

        public long RoleId
        {
            get; set;
        }
        [StringLength(50)]
        public string RoleName { get; set; }
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
        public long BiodataId { get; set; }
        public long UserLoginId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sandi *")]
        public string UserLoginPwd { get; set; }
        public bool check { get; set; }
    }
}
