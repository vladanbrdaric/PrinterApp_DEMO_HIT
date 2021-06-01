using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterAgentLibrary.Models
{
    public class PaperTrayOIDModel
    {
        public string NameOID { get; set; }
        public string MaxCapacityOID  { get; set; }
        public string CurrentLevelOID { get; set; }
    }
}
