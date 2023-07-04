using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MVC_Snek_Attempt.MisterService;
using System.Text.Json.Serialization;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Net;
using MVC_Snek_Attempt.Models;

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
        public Dictionary<string, int> DirectionEndpoint([FromBody] object keyCode)
        {
            var json = JsonSerializer.Serialize(keyCode);

            Dictionary<string, int> directionData = JsonSerializer
                .Deserialize<Dictionary<string, int>>(json);
            
            _game.SetSnekDirectionOnKeyPress(directionData["keyCode"]);

            return directionData;
        }
    }
}
