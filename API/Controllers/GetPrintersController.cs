using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrinterAgentLibrary;
using PrinterAgentLibrary.Models;
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
    public class GetPrintersController : ControllerBase
    {
        private readonly IPrinterSnmpData _printerSnmpData;
        private readonly IDatabaseData _sqliteData;

        // I'm assuming that the snmpCommunity is set to public. This is just a demo.
        string snmpCommunity = "public";

        //List<PrinterModel> printersFromAgent = new List<PrinterModel>();


        // Constructor (dependency injection)
        public GetPrintersController(IPrinterSnmpData printerSnmpData, IDatabaseData sqliteData)
        {
            _printerSnmpData = printerSnmpData;
            _sqliteData = sqliteData;
        }


        //Get all printers from db.
        [HttpGet]
        public async Task<List<PrinterFullModel>> GetAllPrintersFromDBAsync()
        {
            await PostFromAgentToDBAsync();


            return _sqliteData.GetAllPrintersFromDB();
        }

        /// <summary>
        /// Method that get out all ip addresses from the database and store them in a list of type string.
        /// It will run snmp get requests upon each single ip address asynchronously (parallel).
        /// It will store those printer objects in the database.
        /// </summary>
        /// <returns></returns>
        private async Task PostFromAgentToDBAsync()
        {
            List<string> ipAddresses = new List<string>();
            List<Task<PrinterModel>> getPrinterDataTasks = new List<Task<PrinterModel>>();
            List<Task> storeIntoDBTasks = new List<Task>();

            //Get All IP Addresses from DB.
            ipAddresses = _sqliteData.GetIpAddressesFromDB();

            //check if there is any IP address in the DB.
            if (ipAddresses.Count > 0)
            {
                foreach (var ip in ipAddresses)
                {
                    getPrinterDataTasks.Add(Task.Run(() => _printerSnmpData.GetPrinterData(ip, snmpCommunity)));
                }
            }

            var getPrinterDataTasksResult = await Task.WhenAll(getPrinterDataTasks);


            // store printer(s) data from Printer Agent into DB
            foreach (var printer in getPrinterDataTasksResult)
            {
                // check if some printer is "null", if so, don't store that printer into db.
                if (printer != null)
                {
                    storeIntoDBTasks.Add(Task.Run(() => _sqliteData.InsertPrinterValuesFromAgent(printer)));
                }
            }

            await Task.WhenAll(storeIntoDBTasks);
        }
    }
}
