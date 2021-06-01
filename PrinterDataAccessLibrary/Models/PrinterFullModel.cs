using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterAgentLibrary.Models
{
    public class PrinterFullModel
    {
        public string IpAddress { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string UpTime { get; set; }
        public string LifeCount { get; set; }
        public string CountSinceReboot { get; set; }
        public string Color { get; set; }
        public string Status { get; set; } = "";
        public int Black { get; set; } = 0;
        public int Cyan { get; set; } = 0;
        public int Magenta { get; set; } = 0;
        public int Yellow { get; set; } = 0;
        public int Tray1 { get; set; } = 0;
        public int Tray2 { get; set; } = 0;
        public int Tray3 { get; set; } = 0;
        public int Tray4 { get; set; } = 0;
    }
}
