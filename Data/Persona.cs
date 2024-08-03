using Microsoft.Data.SqlClient;

namespace Parcial1.Data
{
    public class Persona
    {
        private readonly string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";

        public string? Cui { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }

        public string GuardarPersona(Persona Persona)
        {
            string qry = @"
                INSERT INTO vehiculos (cui, nombre, apellido, telefono, direccion)
                VALUES (@cui, @nombre, @apellido, @telefono, @direccion)";

            try
            {
                //importante, cargar conector SQL
                using SqlConnection conn = new(connectionString);
                //abrir conexion
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@cui", Persona.Cui);
                cmd.Parameters.AddWithValue("@nombre", Persona.Nombre);
                cmd.Parameters.AddWithValue("@apellido", Persona.Apellido);
                cmd.Parameters.AddWithValue("@anio", Persona.Telefono);
                cmd.Parameters.AddWithValue("@direccion", Persona.Direccion);

                // Execute the command
                cmd.ExecuteNonQuery();
                return "";
            }
            catch (Exception ex)
            {
                // Handle exception
                return (ex.Message);
            }
        }
        public List<Persona> ListarPersonas()
        {
            List<Persona> lista = new();
            string query = "SELECT * FROM personas";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Persona persona = new()
                    {
                        Cui = reader["cui"].ToString() ?? "",
                        Nombre = reader["nombre"].ToString() ?? "",
                        Apellido = reader["apellido"].ToString() ?? "",
                        Telefono = reader["anio"].ToString() ?? "",
                        Direccion = reader["direccion"].ToString() ?? ""
                    };
                    lista.Add(persona);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }

    }
}