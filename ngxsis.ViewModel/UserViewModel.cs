using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ngxsis.ViewModel
{
    public class UserViewModel
    {
        [Display(Name = "Nama Lengkap")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email harus diisi!")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsUsernameUnique","UserRole",AdditionalFields = "Id",ErrorMessage = "Email sudah dipakai!")]
        [Display(Name = "Email *")]
        [EmailAddress(ErrorMessage = "Format email salah!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sandi tidak boleh kosong")]
        [DataType(DataType.Password)]
        [StringLength(16,MinimumLength = 8,ErrorMessage = "Panjang sandi yang diperbolehkan 8 - 16 karakter")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!#\$%&\(\)\*\+,\-\./:;<=>\?@\[\\\]\^_\{\|\}~])[a-zA-Z0-9!#\$%&\(\)\*\+,\-\./:;<=>\?@\[\\\]\^_\{\|\}~]*$",
 ErrorMessage = "Password harus terdiri dari kombinasi angka, huruf kapital, huruf kecil dan symbol")]
        [Display(Name = "Sandi *")]
        //[Remote("IsPassNull", "UserRole", AdditionalFields = "Id",ErrorMessage ="Sandi tidak boleh kosong!")]
        public string Abpwd { get; set; }

        //[Remote("IsPassNull", "UserRole",, AdditionalFields = "Id", ErrorMessage = "Sandi tidak boleh kosong!")]
        [Required(ErrorMessage = "Sandi tidak boleh kosong")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Abpwd",ErrorMessage = "Sandi yang dimasukan tidak sama")]
        [Display(Name = "Ulang Sandi *")]
        public string ConfirmPwd { get; set; }


        public List<UserRoleViewModel> UserRoleList { get; set; }




        //[Required(ErrorMessage = "Username tidak boleh kosong")]
        //[StringLength(50,MinimumLength =8,ErrorMessage = "Username harus terdiri dari 8 - 50 karakter")]
        //[Display(Name = "Username *")]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])[\w@.\[\]-]*$",
        // ErrorMessage = "Username harus terdiri dari kombinasi angka dan huruf")]
        //[Remote("IsUsernameUnique", "UserRole", AdditionalFields = "Id", ErrorMessage = "Username sudah dipakai!")]
        public string Username { get; set; }
        public long? Id { get; set; }//AddrBook_id
        public long BiodataId { get; set; }
        public long UserLoginId { get; set; }

        //[Required(ErrorMessage = "Sandi tidak boleh kosong")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Sandi *")]
        //[Remote("PassVerif","UserRole",AdditionalFields = "UserLoginId",ErrorMessage = "Sandi salah!")]
        //public string UserLoginPwd { get; set; }



    }
}
