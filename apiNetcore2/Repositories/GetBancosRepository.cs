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


    public class GetBancosRepository : IGetBancos
    {

        private readonly IConfiguration _config;
        private RespuestaNoTx Respuesta = new RespuestaNoTx(false, "", "Sin Datos");

        public GetBancosRepository(IConfiguration config)

        {
            _config = config;
        }

        public async Task<RespuestaNoTx> getbancoAsync(GetBancos Trama)
        {

            List<E_Bancos> lista_retorno = new List<E_Bancos>();
            RespuestaNoTx response = new RespuestaNoTx();
            ParametrosSync respuesta = new ParametrosSync();

            DataSet ds = new DataSet();

            try
            {
                
                    Respuesta = new RespuestaNoTx(false, "", "Sin Datos");
                    using (var con = new Connection(_config).SqlConnectionFull())
                    {
                        await con.OpenAsync();
                        using (var cmd = new SqlCommand("sp_getBanco", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@EMPRESA", Trama.Empresa);
                            if (Trama.Fecha != null)
                            {
                                cmd.Parameters.AddWithValue("@Fecha", Trama.Fecha);
                            }
                            DataTable table = new DataTable();
                            table.Load(cmd.ExecuteReader());
                            ds.Tables.Add(table);
                            con.Close();
                            Respuesta.bRespuesta = true;
                            Respuesta.sMensaje = "OK";
                        }
                        if (ds == null)
                        {
                            Respuesta.bRespuesta = false;
                            Respuesta.sMensaje = "Sin Datos";
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //DownloadUsers objetoRetorno = new DownloadUsers();
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    lista_retorno.Add(new E_Bancos(
                                        dr["CODBANCO"].ToString(),
                                        dr["BANCO"].ToString()
                                   ));
                                }
                                Respuesta.bRespuesta = true;
                                Respuesta.Informacion = "Informacion Obtenida con Exito";
                                Respuesta.sMensaje = JsonConvert.SerializeObject(lista_retorno);
                            }
                            else
                            {
                                Respuesta.bRespuesta = false;
                                Respuesta.Informacion = "Sin Datos";
                            }
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