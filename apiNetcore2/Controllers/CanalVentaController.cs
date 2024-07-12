using apiNetcore2.Helpers;
using apiNetcore2.Interface;
using apiNetcore2.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace apiNetcore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanalVentaController : ControllerBase
    {
        private readonly ICanalVenta _canalVenta;
        private readonly IConfiguration _config;

        public CanalVentaController(ICanalVenta canalVenta, IConfiguration config)
        {
            _canalVenta = canalVenta;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetCanalVenta([FromQuery] string accion, [FromQuery] string empresa, [FromQuery] string descripcion = null, [FromQuery] int? idcanalvta = null)
        {
            var trama = new CanalVentaTrama
            {
                Accion = accion,
                Empresa = empresa,
                Descripcion = descripcion,
                IdCanalVta = idcanalvta
            };

            var response = await _canalVenta.GetCanalVentaAsync(trama);
            if (!response.bRespuesta)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
