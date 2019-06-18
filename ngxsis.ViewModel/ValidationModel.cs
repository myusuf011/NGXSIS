using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class ValidationModel
    {

        public DateTime dob { get; set; }
        public string email { get; set; }

        public string phone_number1 { get; set; }

        public long identity_type_id { get; set; }
        public string identity_no { get; set; }
        public string identity_name { get; set; }

    }
}
