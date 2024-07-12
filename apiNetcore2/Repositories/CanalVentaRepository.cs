using apiNetcore2.Entities;
using apiNetcore2.Helpers;
using apiNetcore2.Interface;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Connection = apiNetcore2.Helpers.Connection;

namespace apiNetcore2.Repositories
{
    public class CanalVentaRepository : ICanalVenta
    {
        private readonly IConfiguration _config;
        private RespuestaNoTx Respuesta = new RespuestaNoTx(false, "", "Sin Datos");

        public CanalVentaRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<RespuestaNoTx> GetCanalVentaAsync(CanalVentaTrama trama)
        {
            List<CanalVenta> lista_retorno = new List<CanalVenta>();
            RespuestaNoTx response = new RespuestaNoTx();
            DataSet ds = new DataSet();

            try
            {
                Respuesta = new RespuestaNoTx(false, "", "Sin Datos");
                using (var con = new Connection(_config).SqlConnectionFull())
                {
                    await con.OpenAsync();
                    using (var cmd = new SqlCommand("RED_M_CanalVenta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@accion", trama.Accion);
                        cmd.Parameters.AddWithValue("@empresa", trama.Empresa);
                        if (!string.IsNullOrEmpty(trama.Descripcion))
                        {
                            cmd.Parameters.AddWithValue("@descripcion", trama.Descripcion);
                        }
                        if (trama.IdCanalVta.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@idcanalvta", trama.IdCanalVta.Value);
                        }

                        DataTable table = new DataTable();
                        table.Load(await cmd.ExecuteReaderAsync());
                        ds.Tables.Add(table);
                        con.Close();
                        Respuesta.bRespuesta = true;
                        Respuesta.sMensaje = "OK";
                    }
                    if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        Respuesta.bRespuesta = false;
                        Respuesta.sMensaje = "Sin Datos";
                    }
                    else
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            lista_retorno.Add(new CanalVenta(
                                Convert.ToInt32(dr["idcanalvta"]),
                                dr["descripcion"].ToString(),
                                dr["empresa"].ToString(),
                                Convert.ToInt32(dr["status"])
                            ));
                        }
                        Respuesta.bRespuesta = true;
                        Respuesta.Informacion = "Información obtenida con éxito";
                        Respuesta.sMensaje = JsonConvert.SerializeObject(lista_retorno);
                    }
                }
            }
            catch (Exception e)
            {
                Respuesta.Informacion = e.Message;
                Respuesta.bRespuesta = response.bRespuesta;
                Respuesta.sMensaje = response.sMensaje;
            }

            return Respuesta;
        }
    }
}
