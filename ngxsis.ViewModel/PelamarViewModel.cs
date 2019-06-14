using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class PelamarViewModel
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "")]
        public string fullname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "")]
        public string nick_name { get; set; }

        [StringLength(100)]
        public string major { get; set; }

        [StringLength(100)]
        public string school_name { get; set; }

        [Required]
        [StringLength(100)]
        public string pob { get; set; }

        [Column(TypeName = "date")]
        public DateTime dob { get; set; }

        public bool gender { get; set; }

        public long religion_id { get; set; }

        public int? high { get; set; }

        public int? weight { get; set; }

        [StringLength(100)]
        public string nationality { get; set; }

        [StringLength(50)]
        public string ethnic { get; set; }

        [StringLength(25)]
        public string hobby { get; set; }

        public long identity_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string identity_no { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "")]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "")]
        public string phone_number1 { get; set; }

        [StringLength(50)]
        public string phone_number2 { get; set; }

        [Required]
        [StringLength(50)]
        public string parent_phone_number { get; set; }

        [StringLength(5)]
        public string child_sequence { get; set; }

        [StringLength(5)]
        public string how_many_brothers { get; set; }

        public long marital_status_id { get; set; }

        public long? addrbook_id { get; set; }

        [StringLength(10)]
        public string token { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expired_token { get; set; }

        [StringLength(50)]
        public string marriage_year { get; set; }

        public long company_id { get; set; }
    }
}
