using System;
using System.Collections.Generic;
using System.Text;

namespace PrinterApplicationLibrary.Models
{
    public class PrinterFullModel
    {
        public string IpAddress { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string UpTime { get; set; }
        public string LifeCount { get; set; }
        public string CountSinceReboot { get; set; }
        public string Color { get; set; }
        public string ServiceCode { get; set; }
        public string Status { get; set; }
        public int Black { get; set; }
        public int Cyan { get; set; }
        public int Magenta { get; set; }
        public int Yellow { get; set; }
        public int Tray1 { get; set; }
        public  int Tray2 { get; set; }
        public int Tray3 { get; set; }
        public int Tray4 { get; set; }    
    }
}
