using Microsoft.AspNetCore.Mvc;
using MVC_Snek_Attempt.MisterService;
using MVC_Snek_Attempt.Models;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC_Snek_Attempt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _service;
        private readonly ISnekService _snekService;
        private readonly IConfiguration _configuration;
        public HomeController(
            ILogger<HomeController> logger,
            IGameService service,
            ISnekService snekService, 
            IConfiguration configuration)
        {
            _logger = logger;
            _service = service;
            _snekService = snekService;
            _configuration = configuration;
        }
        
        public async Task<IActionResult> _gridPartial([FromBody] JsonContent grid)
        {
            

            using (var stream = grid.ReadAsStreamAsync())
            {
                var gridList = JsonSerializer.Deserialize<List<List<int>>>(await stream);
                return View(gridList);
            }
        }
        public async Task<IActionResult> Index()
        {
            var theValue = "";

            
            var section = _configuration.GetSection("Endpoints");
            

            ViewBag.Snek = ConfigurationBinder.GetValue<string>(section, "Snek");
            ViewBag.Score = ConfigurationBinder.GetValue<string>(section, "Score");
            ViewBag.Direction = ConfigurationBinder.GetValue<string>(section, "Direction");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region for api endpoints
        
        #endregion
    }
}