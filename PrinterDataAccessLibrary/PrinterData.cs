using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PrinterAgentLibrary.Models;

/*
    http://www.docs.snmpsharpnet.com/docs-0-9-1/html/M_SnmpSharpNet_SimpleSnmp_Get_1.htm <- Source code from this site.
 */


namespace PrinterAgentLibrary
{
    public class PrinterData : IPrinterData
    {
        private readonly IConfiguration _config;

        // ctor
        public PrinterData(IConfiguration config)
        {
            _config = config;
        }


        public List<PrinterFullModel> GetPrintersFromAgent()
        {
            Printers.Printers printerData = new Printers.Printers();

            List<PrinterFullModel> printers = new List<PrinterFullModel>()
            {
                printerData.GetPrinter1(),
                printerData.GetPrinter2(),
                printerData.GetPrinter3(),
                printerData.GetPrinter4(),
                printerData.GetPrinter5(),
                printerData.GetPrinter6(),
                printerData.GetPrinter7()
            };

            return printers;
        }
    }
}
