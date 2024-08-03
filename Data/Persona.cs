using Microsoft.Data.SqlClient;

namespace Parcial1.Data
{
    public class Persona
    {
        private string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";

        public string cui { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }

        public string GuardarPersona(Persona Persona)
        {
            string qry = @"
                INSERT INTO vehiculos (cui, nombre, apellido, telefono, direccion)
                VALUES (@cui, @nombre, @apellido, @telefono, @direccion)";

            try
            {
                //importante, cargar conector SQL
                using SqlConnection conn = new SqlConnection(connectionString);
                //abrir conexion
                conn.Open();
                using SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@cui", Persona.cui);
                cmd.Parameters.AddWithValue("@nombre", Persona.nombre);
                cmd.Parameters.AddWithValue("@apellido", Persona.apellido);
                cmd.Parameters.AddWithValue("@anio", Persona.telefono);
                cmd.Parameters.AddWithValue("@direccion", Persona.direccion);

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
            List<Persona> lista = new List<Persona>();
            string query = "SELECT * FROM personas";
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                using SqlCommand cmd = new SqlCommand(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Persona persona = new Persona();
                    persona.cui = reader["cui"].ToString() ?? "";
                    persona.nombre = reader["nombre"].ToString() ?? "";
                    persona.apellido = reader["apellido"].ToString() ?? "";
                    persona.telefono = reader["anio"].ToString() ?? "";
                    persona.direccion = reader["direccion"].ToString() ?? "";
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