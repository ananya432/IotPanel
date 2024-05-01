using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Model
{
    public class Panel
    {
        public int id { get; set; }
        public int panelId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool? key_1 { get; set; }
        public bool? key_2 { get; set; }
        public bool? key_3 { get; set; }
        public bool? key_4 { get; set; }

    }
    public class PanelDTO
    {
        public int panelId { get; set; }
        public bool? key_1 { get; set; }
        public bool? key_2 { get; set; }
        public bool? key_3 { get; set; }
        public bool? key_4 { get; set; }
    }
}
