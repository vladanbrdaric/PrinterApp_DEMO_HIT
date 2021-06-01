using PrinterAgentLibrary.Models;
using PrinterApplicationLibrary.Models;
using System.Collections.Generic;

namespace PrinterApplicationLibrary.Data
{
    public interface IDatabaseData
    {
        List<PrinterFullModel> GetAllPrintersFromDB();
        List<string> GetIpAddressesFromDB();
        void InsertNewPrinter(PrinterBasicModel printer);
        void InsertPrinterValuesFromAgent(PrinterModel printer);
    }
}