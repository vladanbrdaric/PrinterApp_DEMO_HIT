using PrinterAgentLibrary.Models;
using System.Collections.Generic;

namespace PrinterAgentLibrary
{
    public interface IPrinterData
    {
        List<PrinterFullModel> GetPrintersFromAgent();
    }
}