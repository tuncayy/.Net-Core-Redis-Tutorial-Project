using CodingChallange.Services;
using CodingChallange.ServiceTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallange.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;
        public GameController(IGameService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("Play")]
        public async Task<IActionResult> Game(RequestType request)
        {
            try{
                var response = await _service.PlayGame(request);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Ok(e.Message);
            }
            

        }

    }
}
