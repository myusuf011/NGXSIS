using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngxsis.ViewModel
{
    public class MenuViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        public long? ParentId { get; set; }
        public string Url { get; set; }
        public int Level { get; set; }
        public bool IsDropdown { get; set; }
    }
}
