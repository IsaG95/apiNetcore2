using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;

namespace apiNetcore2.Helpers
{
    public class Connection
    {
        // Define la propiedad para almacenar la configuración de las cadenas de conexión
        private readonly ConnectionStrings _connectionStrings;

        public Connection(IConfiguration config)
        {
            // Aquí se obtiene la configuración de ConnectionStrings y se maneja la posibilidad de que sea null
            _connectionStrings = config.GetSection("ConnectionStrings").Get<ConnectionStrings>()
                               ?? throw new InvalidOperationException("La configuración de las cadenas de conexión no está disponible.");
        }

        // Constructor alternativo no necesario, removido
        // public Connection(object config) { ... }

        /*@
         * conexion con una base de datos
         * @*/
        public SqlConnection SqlConnectionFull(string? database = null)
        {
            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            string user = Environment.GetEnvironmentVariable("DB_USER");
            string password = Environment.GetEnvironmentVariable("DB_PASS");
            string db = database ?? Environment.GetEnvironmentVariable("DB_BASE");

            string connectionString = $"Server={server};Database={db};User Id={user};Password={password}; TrustServerCertificate=True;";

            return new SqlConnection(connectionString);
        }
    }
}
