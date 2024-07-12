using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using apiNetcore2.Entities;
using apiNetcore2.Interface;
using apiNetcore2.Repositories;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace apiNetcore2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetBancosController : ControllerBase
    {
        private readonly IGetBancos _getBancos;
        private readonly IConfiguration _config;

        public GetBancosController(IConfiguration config, IGetBancos getBancos)
        {
            _getBancos = getBancos;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Pago([FromBody] GetBancos banco)
        {
            var respuesta = await _getBancos.getbancoAsync(banco);
            return Ok(respuesta);
        }

    }
}