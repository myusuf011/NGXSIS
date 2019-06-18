using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class LainLainViewModel
    {
        public long biodata_id { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }
                
        public long referensiId { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(100)]
        public string position { get; set; }

        [StringLength(1000)]
        public string address_phone { get; set; }

        [StringLength(100)]
        public string relation { get; set; }

        public long tambahanId { get; set; }

        [StringLength(100)]
        public string emergency_contact_name { get; set; }

        [StringLength(50)]
        public string emergency_contact_phone { get; set; }

        [StringLength(20)]
        public string expected_salary { get; set; }

        public bool? is_negotiable { get; set; }

        [StringLength(10)]
        public string start_working { get; set; }

        public bool? is_ready_to_outoftown { get; set; }

        public bool? is_apply_other_place { get; set; }

        [StringLength(100)]
        public string apply_place { get; set; }

        [StringLength(100)]
        public string selection_phase { get; set; }

        public bool? is_ever_badly_sick { get; set; }

        [StringLength(100)]
        public string disease_name { get; set; }

        [StringLength(100)]
        public string disease_time { get; set; }

        public bool? is_ever_psychotest { get; set; }

        [StringLength(100)]
        public string psychotest_needs { get; set; }

        [StringLength(100)]
        public string psychotest_time { get; set; }

        [StringLength(500)]
        public string requirementes_required { get; set; }

        [StringLength(1000)]
        public string other_notes { get; set; }

    }
}
