using apiNetcore2.Entities;

namespace apiNetcore2.Interface
{
    public interface ICanalVenta
    {
        Task<RespuestaNoTx> GetCanalVentaAsync(CanalVentaTrama trama);
    }
}

