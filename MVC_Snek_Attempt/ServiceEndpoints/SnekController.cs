using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MVC_Snek_Attempt.MisterService;
using System.Text.Json.Serialization;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Net;

namespace MVC_Snek_Attempt.ServiceEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnekController : ControllerBase
    {
        private readonly IGameService _game;
        public SnekController(IGameService game)
        {
            _game = game;
        }
        
        [HttpGet(nameof(SnekEndpoint))]
        public async Task<string> SnekEndpoint()
        {
            var grid = _game.UpdateAndGetGrid();
            
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync<List<List<int>>>(stream, grid);
                var bits = stream.ToArray();
                string jsonGrid = Encoding.UTF8.GetString(bits);
                return jsonGrid;
            }
        }

        [HttpPost(nameof(DirectionEndpoint))]
        public HttpStatusCode DirectionEndpoint([FromBody] object keyCode)
        {
            
            
            return (HttpStatusCode.OK);
        }
    }
}
