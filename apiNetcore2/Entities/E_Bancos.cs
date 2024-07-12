namespace apiNetcore2.Entities
{
    public class E_Bancos
    {
        public string? ID { get; set; }
        public string? DESCRIPCION { get; set; }

        public E_Bancos(string? iD, string? dESCRIPCION)
        {
            ID = iD;
            DESCRIPCION = dESCRIPCION;
        }
    }
}
