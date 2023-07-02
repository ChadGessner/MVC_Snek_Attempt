using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MVC_Snek_Attempt.MisterService;
using System.Text.Json.Serialization;
using System.Text;

namespace MVC_Snek_Attempt.ServiceEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnekController : ControllerBase
    {
        public ISnekService SnekService { get; set; }
        public IGridService GridService { get; set; }
        public SnekController(ISnekService theOnlySnekRight, IGridService gridService)
        {
            SnekService = theOnlySnekRight;
            GridService = gridService;
        }
        [HttpGet(nameof(CanHas))]
        public async Task<object> CanHas()
        {
            List<int> snek = SnekService.Triggered();
            using (var stream = new MemoryStream())
            {

                await JsonSerializer
                    .SerializeAsync<List<int>>(stream, snek);
                byte[] bits = stream.ToArray();
                string jsonGrid = Encoding.UTF8.GetString(bits);
                
                return jsonGrid;
            }
            
        }
        [HttpGet(nameof(SnekEndpoint))]
        public async Task<string> SnekEndpoint()
        {
            var snek = SnekService.Triggered();
            var grid = GridService.GetGrid();
            await Console.Out.WriteLineAsync("is this hitting?");
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync<List<List<int>>>(stream, grid);
                var bits = stream.ToArray();
                string jsonGrid = Encoding.UTF8.GetString(bits);
                return jsonGrid;
            }
        }
    }
}
