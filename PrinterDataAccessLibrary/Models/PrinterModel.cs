using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterAgentLibrary.Models
{
    public class PrinterModel
    {
        public string IpAddress { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string UpTime { get; set; }
        public string LifeCount { get; set; }
        public string CountSinceReboot { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public TonerColorModel Toner1ColorInPercentage { get; set; }
        public TonerColorModel Toner2ColorInPercentage { get; set; }
        public TonerColorModel Toner3ColorInPercentage { get; set; }
        public TonerColorModel Toner4ColorInPercentage { get; set; }
        public PaperTrayModel Tray1InPercentage { get; set; }
        public PaperTrayModel Tray2InPercentage { get; set; }
        public PaperTrayModel Tray3InPercentage { get; set; }
        public PaperTrayModel Tray4InPercentage { get; set; }
    }
}
