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

        [HttpGet("{name?}")]
        public async Task<IActionResult> Get([FromQuery]string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = Guid.NewGuid().ToString();
            }

            return Ok(await _service.RegisterValetKey(name));
        }

        public ValetKeysController(IValetKeyService service)
        {
            _service = service;
        }

    }
}