using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class ProsesPelamarViewModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string SchoolName { get; set; }
        public string Major { get; set; }
        public bool? IsProcess { get; set; }
        public bool? IsComplete { get; set; }
    }
    public class UndanganViewModel
    {
        public long Id { get; set; }
        public long UndanganId { get; set; }
        public long? ScheduleTypeId { get; set; }
        [Display(Name = "Jenis Jadwal")]
        public string ScheduleTypeName { get; set; }
        public string InvitationDateStr { get; set; }
        public string InvitationTime { get; set; }
        public long? RoId { get; set; }
        [Display(Name ="RO")]
        public string RoName { get; set; }
        public long? TroId { get; set; }
        [Display(Name = "TRO")]
        public string TroName { get; set; }
        [Display(Name = "Lokasi")]
        public string Location { get; set; }
    }
    public class JadwalViewModel
    {
        public long Id { get; set; }
        public long? JadwalId { get; set; }

        public long? ScheduleTypeId { get; set; }
        [Display(Name = "Jenis Undangan")]
        public string ScheduleTypeName { get; set; }
        public string ScheduleDateStr { get; set; }
        public string ScheduleTime { get; set; }
        public long? RoId { get; set; }
        [Display(Name = "RO")]
        public string RoName { get; set; }
        public long? TroId { get; set; }
        [Display(Name = "TRO")]
        public string TroName { get; set; }
        [Display(Name = "Lokasi")]
        public string Location { get; set; }
    }
}
