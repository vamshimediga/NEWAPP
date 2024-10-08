using AutoMapper;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using Newtonsoft.Json;

namespace NEWAPP.Controllers
{
    public class StockSymbolController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper; // Inject AutoMapper
        private readonly HttpClient _httpClient;

        public StockSymbolController(IConfiguration configuration, IMapper mapper, HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _mapper = mapper;
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<StockViewModel> stockViewModel = new List<StockViewModel>();
            string URL = _configuration["StockSymbol:StockSymbol_US"];
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{URL}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                List<Stock> stocks = JsonConvert.DeserializeObject<List<Stock>>(jsonResponse);
                stockViewModel = _mapper.Map<List<StockViewModel>>(stocks.Take(100).ToList());
            }
            return View("StockList", stockViewModel);

        }

       
        public async Task<IActionResult> GetStockList()
        {
            List<StockViewModel> stockViewModel = new List<StockViewModel>();
            string URL = _configuration["StockSymbol:StockSymbol_US"];
            // Use HttpClient from the factory
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.GetAsync(URL);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                List<Stock> stocks = JsonConvert.DeserializeObject<List<Stock>>(jsonResponse);

                // Map only the first 10 records
                stockViewModel = _mapper.Map<List<StockViewModel>>(stocks.Take(100).ToList());
            }

            return View("StockList", stockViewModel);

        }
    }
}
