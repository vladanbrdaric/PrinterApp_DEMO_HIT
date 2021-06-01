using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using PrinterAgentLibrary.Models;
using PrinterApplicationLibrary.Data;
using PrinterApplicationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddPrinterController : ControllerBase
    {
        private readonly IDatabaseData _sqliteData;

        // Constructor (object instances provided via dependency injection)
        public AddPrinterController(IDatabaseData sqliteData)
        {
            _sqliteData = sqliteData;
        }

        // Post method that get an object of type PrinterBasicModel via http post request, and then it call a method that is located in business logic layer.
        [HttpPost]
        public void InsertIntoDB(PrinterBasicModel printer)
        {
            _sqliteData.InsertNewPrinter(printer);
        }
    }
}
