using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class ProfilViewModel
    {
        public long id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public bool Gender { get; set; }

        public string EntryYear { get; set; }
        public string GraduationYear { get; set; }
        public string Major { get; set; }
        public double ? GPA { get; set; }
        public string Country { get; set; }
        public string SchoolName { get; set; }


        public string CompanyName { get; set; }
        public string JoinYear { get; set; }
        public string ResignYear { get; set; }
        public string LastPosition { get; set; }
        public string Income { get; set; }


        public string SkillName { get; set; }
        public string SkillLevel { get; set; }


        public string TrainingName { get; set; }
        public string TrainingYear { get; set; }

        public string ValidStartYear { get; set; }
        public string UntilYear { get; set; }
        public string CertificateName { get; set; }
    }
}
