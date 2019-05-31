using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class ResponseResultLogin
    {
        public ResponseResultLogin()
        {
            Success = true;
            GagalLogin = false;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public bool GagalLogin { get; set; }

        public long AkunID { get; set; }

        public string NamaAkun { get; set; }

        public DateTime? TanggalUbah { get; set; }

        public bool Blokir { get; set; }
    }
}
