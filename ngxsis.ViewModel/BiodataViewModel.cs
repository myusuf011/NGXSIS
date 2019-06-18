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

    public class BiodataViewModel
    {

        public long id { get; set; }
        [Required(ErrorMessage = "Silahkan Isi Kolom Nama Lengkap Terlebih Dahulu!")]
        [StringLength(255)]
        [Display(Name = "Nama Lengkap *")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Nama Panggilan Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Nama Panggilan *")]
        public string nick_name { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Tempat Lahir Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Tempat Lahir *")]
        public string pob { get; set; }


        [Required(ErrorMessage = "Silahkan Isi Kolom Tanggal Lahir Terlebih Dahulu!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tgl. Lahir (yyyy-mm-dd) *")]

        public DateTime dob { get; set; }


        [Required(ErrorMessage = "Silahkan Isi Kolom Jenis Kelamin Terlebih Dahulu!")]
        [Display(Name = "Jenis kelamin *")]
        public bool gender { get; set; }


        [Required(ErrorMessage = "Silahkan Isi Kolom Agama Terlebih Dahulu!")]
        [Display(Name = "Agama *")]
        public long religion_id { get; set; }

        [Required(ErrorMessage = "Silahkan Isi tinggi Terlebih Dahulu!")]
        [Display(Name = "Tinggi (cm) *")]
        public int? high { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Berat Terlebih Dahulu!")]
        [Display(Name = "Berat (kg) *")]
        public int? weight { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Kewarganegaraan Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Kewarganegaraan *")]
        public string nationality { get; set; }

        [StringLength(50)]
        [Display(Name = "Suku Bangsa")]
        public string ethnic { get; set; }

        [StringLength(25)]
        [Display(Name = "Kegemaran / Hobi")]
        public string hobby { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Jenis Identitas Terlebih Dahulu!")]
        [Display(Name = "Jenis Identitas *")]
        public long identity_type_id { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Nomor Identitas Terlebih Dahulu!")]
        [StringLength(50)]
        [Remote("VerifyIdentity", "Pelamar", AdditionalFields = "identity_type_id, id", HttpMethod = "POST")]
        [Display(Name = "Nomor Identitas")]
        public string identity_no { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Email Terlebih Dahulu!")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Silahkan Isi Format Email dengan Benar!")]
        [Remote("VerifyEmail", "Pelamar", AdditionalFields = "id", HttpMethod = "POST", ErrorMessage = "Email Telah Terdaftar!. Silahkan Isi dengan Email yang Berbeda")]
        [Display(Name = "Email *")]

        public string email { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom No. Hp Terlebih Dahulu!")]
        [StringLength(50)]
        [Remote("VerifyPhone", "Pelamar", AdditionalFields = "id", HttpMethod = "POST", ErrorMessage = "No. HP Telah Terdaftar!. Silahkan Isi dengan No. HP yang Berbeda")]
        [Display(Name = "No. HP *")]
        public string phone_number1 { get; set; }

        [StringLength(50)]
        [Display(Name = "No. HP Alternatif")]
        public string phone_number2 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom No. Tlp (Rumah/Orang Tua) Terlebih Dahulu!")]
        [StringLength(50)]
        [Display(Name = "No. Tlp (Rumah/Orang Tua) *")]
        public string parent_phone_number { get; set; }

        
        [Required(ErrorMessage = "Silahkan Isi Kolom Anak Ke- Terlebih Dahulu!")]
        [StringLength(5)]
        [Display(Name = "Anak Ke *")]
        public string child_sequence { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Berapa Bersaudara Terlebih Dahulu!")]
        [StringLength(5)]
        [Display(Name = "Dari Berapa Bersaudara *")]
        public string how_many_brothers { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Status Pernikahan Terlebih Dahulu!")]
        [Display(Name = "Status Pernikahan *")]
        public long marital_status_id { get; set; }


        [StringLength(50)]
        [Display(Name = "Tahun Pernikahan")]
        public string marriage_year { get; set; }

        public long company_id { get; set; }


        [Required(ErrorMessage = "Silahkan Isi Kolom Alamat Terlebih Dahulu!")]
        [StringLength(1000)]
        [Display(Name = "Alamat *")]
        public string address1 { get; set; }

        public long biodata_id { get; set; }

        [StringLength(20)]
        [Display(Name = "Kode Pos")]
        public string postal_code1 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom RT Terlebih Dahulu!")]
        [StringLength(5)]
        [Display(Name = "RT *")]
        public string rt1 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom RW Terlebih Dahulu!")]
        [StringLength(5)]
        [Display(Name = "RW *")]
        public string rw1 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Desa / Kelurahan Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Desa / Kelurahan *")]
        public string kelurahan1 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Kecamatan Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Kecamatan *")]
        public string kecamatan1 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Kota Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Kota *")]
        public string region1 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Alamat Terlebih Dahulu!")]
        [StringLength(1000)]
        [Display(Name = "Alamat *")]
        public string address2 { get; set; }


        [StringLength(20)]
        [Display(Name = "Kode Pos")]
        public string postal_code2 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom RT Terlebih Dahulu!")]
        [StringLength(5)]
        [Display(Name = "RT *")]
        public string rt2 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom RW Terlebih Dahulu!")]
        [StringLength(5)]
        [Display(Name = "RW *")]
        public string rw2 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Desa / Kelurahan Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Desa / Kelurahan *")]
        public string kelurahan2 { get; set; }


        [Required(ErrorMessage = "Silahkan Isi Kolom Kecamatan Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Kecamatan *")]
        public string kecamatan2 { get; set; }

        [Required(ErrorMessage = "Silahkan Isi Kolom Kota Terlebih Dahulu!")]
        [StringLength(100)]
        [Display(Name = "Kota *")]
        public string region2 { get; set; }


        public string namaidentitas { get; set; }
        public string namagender { get; set; }
        public string namaagama { get; set; }
        public string namastatus { get; set; }
        public long user_id { get; set; }
        public string tanggal { get; set; }



    }
}
