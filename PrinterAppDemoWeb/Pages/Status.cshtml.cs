using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PrinterAppDemoWeb.Models;

namespace PrinterApplicationWebUI.Pages
{
    public class StatusModel : PageModel
    {
        private readonly ILogger<StatusModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public List<PrinterFullModel> Printers { get; set; } = new List<PrinterFullModel>();
        //public List<PrinterModel> Printers { get; set; } = new List<PrinterModel>();

        public StatusModel(ILogger<StatusModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }


        public async Task OnGet()
        {
            Printers = await GetPrinters();
        }


        private async Task<List<PrinterFullModel>> GetPrinters()
        {
            var _client = _httpClientFactory.CreateClient();

            var response = await _client.GetAsync("http://localhost:56776/api/GetPrinters");

            List<PrinterFullModel> printers;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                string responseText = await response.Content.ReadAsStringAsync();

                printers = JsonSerializer.Deserialize<List<PrinterFullModel>>(responseText, options);

                return printers;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
