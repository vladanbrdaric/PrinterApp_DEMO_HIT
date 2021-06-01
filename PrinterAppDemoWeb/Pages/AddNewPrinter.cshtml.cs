using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PrinterAppDemoWeb.Models;

namespace PrinterAppDemoWeb.Pages
{
    public class AddNewPrinterModel : PageModel
    {
        private readonly ILogger<AddNewPrinterModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        // Values from the form 'AddNewPrinter' page will be stored in this object and posted to the db.

        public PrinterBasicModel Printer { get; set; } = new PrinterBasicModel();

        [BindProperty]
        //[Required]
        public string IpAddress { get; set; }

        [BindProperty]
        //[Required]
        public string Description { get; set; }

        [BindProperty]
        //[Required]
        public string Location { get; set; }


        // ctor
        public AddNewPrinterModel(ILogger<AddNewPrinterModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Printer.IpAddress = IpAddress;
                Printer.Description = Description;
                Printer.Location = Location;

                await StorePrinter(Printer);
                return new RedirectToPageResult("ChooseWhereToGo"); 
            }
            else
            {
                return Page();
            }
        }


        // method that store a new printer into DB through the API http post request
        private async Task StorePrinter(PrinterBasicModel printer)
        {
            var _client = _httpClientFactory.CreateClient();

            // convert object of type PrinterBasicModel into json format
            var response = await _client.PostAsync(
                "http://localhost:56776/api/AddPrinter",                  // type of text is "application/json"
                new StringContent(JsonSerializer.Serialize(printer), Encoding.UTF8, "application/json"));

            /*"http://localhost:61131/api/PrinterApplicationUI",  */                // type of text is "application/json   http://localhost:56776"
        }
    }
}
