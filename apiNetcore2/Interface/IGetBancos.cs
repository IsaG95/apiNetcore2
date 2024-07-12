using apiNetcore2.Entities;
using System.Threading.Tasks;

namespace apiNetcore2.Interface
{
    public interface IGetBancos
    {
        Task<RespuestaNoTx> getbancoAsync(GetBancos Trama);
    }
}
