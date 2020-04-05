using Css.Main.Services;
using Css.Shared.Events;
using Css.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Css.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MainController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        private readonly IShelvesService _shelvesService;

        private readonly ILogger<MainController> _logger;

        public MainController(IMessagingService messagingService, IShelvesService shelvesService, ILogger<MainController> logger)
        {
            _messagingService = messagingService;
            _shelvesService = shelvesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> StartOrders()
        {
            await _messagingService.Publish(new StartOrders());
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> StopOrders()
        {
            await _messagingService.Publish(new StopOrders());
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetShelves()
        {
            return Ok(JsonConvert.SerializeObject(await _shelvesService.GetShelves()));
        }
    }
}
