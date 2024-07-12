namespace apiNetcore2.Entities
{
    public class CanalVenta
    {
        public int IdCanalVta { get; set; }
        public string Descripcion { get; set; }
        public string Empresa { get; set; }
        public int Status { get; set; }

        public CanalVenta(int idCanalVta, string descripcion, string empresa, int status)
        {
            IdCanalVta = idCanalVta;
            Descripcion = descripcion;
            Empresa = empresa;
            Status = status;
        }
    }
}
