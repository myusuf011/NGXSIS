using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class KeteranganTambahanViewModel
    {
        public long biodata_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public long tambahanId { get; set; }

        [StringLength(100)]
        [Display(Name = "Kontak Darurat (Keluarga Tidak Serumah) *")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string emergency_contact_name { get; set; }

        [StringLength(50)]
        [Display(Name = "Nomor Telp. Darurat *")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Enter only numeric number")]
        public string emergency_contact_phone { get; set; }

        [StringLength(20)]
        [Display(Name = "Gaji yang Diharapkan (IDR) *")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Enter only numeric number")]
        public string expected_salary { get; set; }

        [Display(Name = "Negotiable")]
        public bool? is_negotiable { get; set; }

        [StringLength(100)]
        [Display(Name = "Kapan Siap Mulai Bekerja *")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string start_working { get; set; }

        [Display(Name = "Bersedia Ditempatkan di Luar Kota")]
        public bool? is_ready_to_outoftown { get; set; }

        [Display(Name = "Sedang Melamar Ditempat Lain")]
        public bool? is_apply_other_place { get; set; }

        [StringLength(100)]
        [Display(Name = "Dimana (Tempat Melamar) ?")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string apply_place { get; set; }

        [StringLength(100)]
        [Display(Name = "Tahapan Seleksi Melamar")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string selection_phase { get; set; }

        [Display(Name = "Pernah Sakit Berat / Lama Sembuh")]
        public bool? is_ever_badly_sick { get; set; }

        [StringLength(100)]
        [Display(Name = "Nama Penyakit")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string disease_name { get; set; }

        [StringLength(100)]
        [Display(Name = "Kapan (Waktu Sakit) ?")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string disease_time { get; set; }

        [Display(Name = "Pernah Mengikuti Psikotes")]
        public bool? is_ever_psychotest { get; set; }

        [StringLength(100)]
        [Display(Name = "Keperluan Psikotes")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string psychotest_needs { get; set; }

        [StringLength(100)]
        [Display(Name = "Kapan (Waktu Psikotes) ?")]
        [Required(ErrorMessage = "Kolom harus diisi")]
        public string psychotest_time { get; set; }

        [StringLength(500)]
        [Display(Name = "Persyaratan yang Diperlukan untuk Jabatan yang Dilamar (menurut kandidat/karyawan)")]
        [DataType(DataType.MultilineText)]
        public string requirementes_required { get; set; }

        [StringLength(1000)]
        [Display(Name = "Keterangan Lain yang ingin Dikemukakan")]
        [DataType(DataType.MultilineText)]
        public string other_notes { get; set; }
    }
}
