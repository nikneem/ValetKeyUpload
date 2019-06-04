using System;
using System.Threading.Tasks;
using IntelligentBusinessApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentBusinessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValetKeysController : ControllerBase
    {
        private readonly IValetKeyService _service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var blob = Guid.NewGuid();
            return Ok(await _service.RegisterValetKey(blob));
        }

        public ValetKeysController(IValetKeyService service)
        {
            _service = service;
        }

    }
}