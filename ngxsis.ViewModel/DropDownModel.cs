using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
   public class DropDownModel
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(100)]
        public string description { get; set; }



    }
}
