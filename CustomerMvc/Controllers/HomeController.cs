using CustomerMvc.Models;
using CustomerMvc.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _iTokenService;
        public HomeController(ITokenService iTokenService,ILogger<HomeController> logger)
        {
            _iTokenService = iTokenService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Customer()
        {
            var data = new List<Customer>();


            using (var client = new HttpClient())
            {
                var tokenResponse = await _iTokenService.GetToken(scope: "scope1");
                client.SetBearerToken(tokenResponse.AccessToken);

                var result = client
                  .GetAsync("http://localhost:5445/Customer")
                  .Result;
               

                if (result.IsSuccessStatusCode)
                {
                    var model = result.Content.ReadAsStringAsync().Result;

                    data = JsonConvert.DeserializeObject<List<Customer>>(model);

                    return View(data);
                }
                else
                {
                    throw new Exception("Unable to get content");
                }
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
