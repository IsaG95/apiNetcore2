using Newtonsoft.Json;

namespace apiNetcore2.Entities
{
    public class ResponseT
    {
        public int Status { get; set; } // Resultado de la operacion
        public object? Data { get; set; }
        public string? Message { get; set; } //informacion de la operacion

        public ResponseT() { }

        public ResponseT(int _status, object? json, string? _message)
        {
            this.Status = _status;
            this.Data = json;
            this.Message = _message;
        }
        public ResponseT(object? json, string? _message)
        {
            this.Data = json;
            this.Message = _message;
        }

        public ResponseT(object? json)

        {
            this.Data = json;
        }
        public void NotFound()
        {
            this.Status = 404;
            this.Data = default(object?);
            this.Message = "No encontrado";
        }


    }
    public class ResPagosCuenta
    {
        public string Recibo { get; set; }
        public string NoTx { get; set; }
        public string Serie { get; set; }
    }

    public class Response
    {
        public object? data { get; set; }
        public int status { get; set; }
        public string? message { get; set; }
        public Response Result { get; internal set; }
        public bool bRespuesta { get; set; }

        public Response() { }

        public Response(bool bRespuesta, object? data, int status, string? message)
        {
            this.bRespuesta = bRespuesta;
            this.data = data;
            this.status = status;
            this.message = message;
        }
    }

    public class ParametrosSync
    {
        public ParametrosSync()
        {
        }
        public ParametrosSync(string? empresa, string? tienda, string? caja, string? noTx)
        {
            this.Empresa = empresa;
            this.Tienda = tienda;
            this.Caja = caja;
            this.NoTx = noTx;
        }
        public string? Empresa { get; set; }
        public string? Tienda { get; set; }
        public string? Caja { get; set; }
        public string? NoTx { get; set; }
        public string? Token { get; set; }
        public string? Serial { get; set; }
        public string? Fecha { get; set; }
        public DateTime? FechaDateTime { get; set; }
        public string? TIPO { get; set; }
    }

    public class ParametrosDescXCliente
    {
        public ParametrosDescXCliente() { }
        public ParametrosDescXCliente(string empresa, string accion, string codigo, int tipoDesc, string categoria, string token)
        {
            Empresa = empresa;
            Accion = accion;
            Codigo = codigo;
            TipoDesc = tipoDesc;
            Categoria = categoria;
            Token = token;
        }

        public string Empresa { get; set; }
        public string Accion { get; set; }
        public string Codigo { get; set; }
        public int TipoDesc { get; set; }
        public string Categoria { get; set; }
        public string Token { get; set; }
    }

    public class Respuesta
    {
        public bool bRespuesta { get; set; }
        public string? sMensaje { get; set; }

    }

    public class ResponseDatos
    {
        public ResponseDatos()
        {

        }
        public ResponseDatos(bool success, String Info, String data)
        {
            //this.bRespuesta = status;
            //this.Informacion = Info;
            //this.sMensaje = data;
            this.Success = success;
            this.Status = (success) ? 200 : 400;
            this.Message = Info;
            this.Data = data;
        }
        public ResponseDatos(int status, String Info, String data)
        {
            this.Status = status;
            this.Message = Info;
            this.Data = data;
        }
        //public bool bRespuesta { get; set; }
        //public string sMensaje { get; set; }
        //public string Informacion { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public bool Success { get; set; }
    }
    public class RespuestaNoTx
    {


        public RespuestaNoTx() { }



        public RespuestaNoTx(bool bRespuesta, string sMensaje, string informacion)
        {
            this.bRespuesta = bRespuesta;
            this.sMensaje = sMensaje;
            this.Informacion = informacion;
        }

        /*public RespuestaNoTx(RespuestaNoTx retorno)
        {
            Retorno = retorno;
        }*/

        public bool bRespuesta { get; set; }
        public string? sMensaje { get; set; }
        public string? Informacion { get; set; }
        //public RespuestaNoTx Retorno { get; }

        public void SetRespuesta(bool bRespuesta, string? sMensaje, string? Informacion)
        {
            this.bRespuesta = bRespuesta;
            this.sMensaje = sMensaje;
            this.Informacion = Informacion;
        }
        public void SetResponseSuccess(object obj)
        {
            this.bRespuesta = true;
            this.sMensaje = (obj != null) ? JsonConvert.SerializeObject(obj) : "";
            this.Informacion = "Finalizado con exito";
        }
    }
}

